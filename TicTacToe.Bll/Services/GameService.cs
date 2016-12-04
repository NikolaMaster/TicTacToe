﻿using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Bll.Dto;
using TicTacToe.Bll.Infrastructure;
using TicTacToe.Bll.Interfaces;
using TicTacToe.Dal.Entities;
using TicTacToe.Dal.Interfaces;

namespace TicTacToe.Bll.Services
{
    public class GameService : IGameService
    {
        private IUnitOfWork _database { get; }
        private const int GameCapacity = 3;

        public GameService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public GameDto MakeAiTurn(int id)
        {
            var game = getGame(id);
            validateGame(game);

            var aiTurn = new TurnDto
            {
                GameId = id
            };
            
            var state = getGameState(game);
            for (var x = 0; x < state.GetLength(0); x++)
            {
                for (var y = 0; y < state.GetLength(1); y++)
                {
                    if (!string.IsNullOrEmpty(state[x, y]))
                    {
                        continue;
                    }

                    aiTurn.X = x;
                    aiTurn.Y = y;
                    break;
                }
            }

            return makeTurn(aiTurn, game);
        }

        public GameDto MakeTurn(TurnDto turnDto)
        {
            var game = turnDto.GameId.HasValue ? _database.Games.Get(turnDto.GameId.Value) : new Game();
            validateGame(game);
            return makeTurn(turnDto, game);
        }

        public GameDto GetGame(int id)
        {
            var game = getGame(id);
            return convertGameEntityToDto(game);
        }

        public IEnumerable<GameDto> GetGames()
        {
            return _database.Games.GetAll().Select(convertGameEntityToDto);
        }

        public void DeleteGame(int id)
        {
            _database.Games.Delete(id);
            _database.Save();
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        private GameDto makeTurn(TurnDto turnDto, Game game)
        {
            game.Game2Players.Add(new Game2Player
            {
                Date = DateTime.Now,
                PlayerId = turnDto.PlayerId,
                X = turnDto.X,
                Y = turnDto.Y,
                GameId = turnDto.GameId ?? 0
            });

            if (game.Id > 0)
            {
                _database.Games.Update(game);
            }
            else
            {
                _database.Games.Create(game);
            }

            _database.Save();
            return convertGameEntityToDto(game);
        }

        private Game getGame(int id)
        {
            var game = _database.Games.Get(id);
            if (game == null)
            {
                throw new ValidationException("Game was not found", string.Empty);
            }

            return game;
        }

        private static GameDto convertGameEntityToDto(Game game)
        {
            return new GameDto(GameCapacity)
            {
                Id = game.Id,
                IsFinished = game.IsFinished,
                State = getGameState(game)
            };
        }

        private static string[,] getGameState(Game game)
        {
            var gameState = new string[GameCapacity, GameCapacity];
            var turns = game.Game2Players.OrderByDescending(g2P => g2P.Date).ToArray();
            for (var i = 0; i < turns.Length; i++)
            {
                var symbol = (i + 1) % 2 == 0 ? "X" : "0";
                var turn = turns[i];
                gameState[turn.X, turn.Y] = symbol;
            }

            return gameState;
        }

        private static void validateGame(Game game)
        {
            if (!game.IsFinished)
            {
                return;
            }

            var winner = game.Game2Players.OrderByDescending(g2P => g2P.Date).First().Player;
            throw new ValidationException(
                string.Format("Game has been already finished. '{0}' player has won.", winner.Name), string.Empty);
        }
    }
}
