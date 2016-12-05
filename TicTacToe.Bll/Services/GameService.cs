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
                game.IsFinished = _gameResultChecker.DoesGameFinished(getGameState(game));
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

        private static byte[,] getGameState(Game game)
        {
            var gameState = new byte[GameCapacity, GameCapacity];
            var turns = game.Game2Players.OrderBy(g2P => g2P.Date).ToArray();
            var firstPlayerId = turns.First().PlayerId;
            foreach (var turn in turns)
            {
                var symbol = Convert.ToByte(turn.PlayerId == firstPlayerId ? 2 : 1);
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
                    if (state[x, y] > 0)
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
