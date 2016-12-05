using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Bll.BusinessModel;
using TicTacToe.Bll.Dto;
using TicTacToe.Bll.Infrastructure;
using TicTacToe.Bll.Interfaces;
using TicTacToe.Dal.Entities;
using TicTacToe.Dal.Interfaces;

namespace TicTacToe.Bll.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _database;
        private const int GameCapacity = 3;
        private readonly GameResultChecker _gameResultChecker;

        public GameService(IUnitOfWork uow)
        {
            _database = uow;
            _gameResultChecker = new GameResultChecker();
        }

        public GameDto MakeAiTurn(int id)
        {
            var game = getGame(id);
            validateGame(game);

            var aiTurn = getAiTurn(game);
            return makeTurn(aiTurn, game);
        }

        public GameDto MakeTurn(TurnDto turnDto)
        {
            var game = turnDto.GameId.HasValue && turnDto.GameId.Value > 0
                ? _database.Games.Get(turnDto.GameId.Value)
                : new Game();
            validateGame(game);
            return makeTurn(turnDto, game);
        }

        public GameDto GetGame(int id)
        {
            var game = getGame(id);
            return convertToDto(game);
        }

        public IEnumerable<GameDto> GetGames()
        {
            return _database.Games.GetAll().Select(convertToDto);
        }

        public GameDto DeleteGame(int id)
        {
            var game = _database.Games.Get(id);
            if (game == null)
            {
                return null;
            }

            _database.Games.Delete(id);
            _database.Save();
            return convertToDto(game);
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
                var gameState = getGameState(game);
                if (gameState[turnDto.X, turnDto.Y] != 0)
                {
                    throw new CustomValidationException("Requested cell already contains value", string.Empty);
                }

                game.IsFinished = _gameResultChecker.DoesGameFinished(gameState);
                _database.Games.Update(game);
            }
            else
            {
                _database.Games.Create(game);
            }

            _database.Save();

            return convertToDto(game);
        }

        private Game getGame(int id)
        {
            var game = _database.Games.Get(id);
            if (game == null)
            {
                throw new CustomValidationException("Game was not found", string.Empty);
            }

            return game;
        }

        private static GameDto convertToDto(Game game)
        {
            return new GameDto(GameCapacity)
            {
                Id = game.Id,
                IsFinished = game.IsFinished,
                State = getGameState(game)
            };
        }

        private static sbyte[,] getGameState(Game game)
        {
            var gameState = new sbyte[GameCapacity, GameCapacity];
            var turns = game.Game2Players.OrderBy(g2P => g2P.Date).ToArray();
            if (!turns.Any())
            {
                return gameState;
            }

            var firstPlayerId = turns.First().PlayerId;
            foreach (var turn in turns)
            {
                var symbol = Convert.ToSByte(turn.PlayerId == firstPlayerId ? 1 : -1);
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
            throw new CustomValidationException(
                string.Format("Game has been already finished. '{0}' player has won.", winner.Name), string.Empty);
        }

        private static TurnDto getAiTurn(Game game)
        {
            var res = new TurnDto
            {
                GameId = game.Id
            };

            var state = getGameState(game);
            for (var x = 0; x < state.GetLength(0); x++)
            {
                for (var y = 0; y < state.GetLength(1); y++)
                {
                    if (state[x, y] != 0)
                    {
                        continue;
                    }

                    res.X = x;
                    res.Y = y;
                    return res;
                }
            }

            return res;
        }
    }
}
