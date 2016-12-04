using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicTacToe.Bll.Dto;
using TicTacToe.Bll.Services;
using TicTacToe.Dal.Entities;
using TicTacToe.Dal.Interfaces;

namespace TicTacToe.Bll.Test
{
    [TestClass]
    public class GameServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<Game>> _gameRepository;
        private readonly Game _game;

        public GameServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _gameRepository = new Mock<IRepository<Game>>();
            _game = new Game
            {
                Id = 1,
                IsFinished = false,
                Game2Players = new List<Game2Player>
                {
                    new Game2Player
                    {
                        Id = 1,
                        Date = DateTime.Now,
                        GameId = 1,
                        PlayerId = 1,
                        X = 0,
                        Y = 0
                    },
                    new Game2Player
                    {
                        Id = 2,
                        Date = DateTime.Now,
                        GameId = 1,
                        PlayerId = 1,
                        X = 1,
                        Y = 1
                    }
                }
            };
        }

        [TestMethod]
        public void TestMakeTurn()
        {
            //Arrange
            _gameRepository.Setup(gr => gr.Get(It.IsAny<int>())).Returns(_game);
            _unitOfWorkMock.SetupGet(uow => uow.Games).Returns(_gameRepository.Object);
            var gameService = new GameService(_unitOfWorkMock.Object);
            var turnDto = new TurnDto
            {
                GameId = 1,
                PlayerId = 1,
                X = 2,
                Y = 2
            };

            //Act
            var result = gameService.MakeTurn(turnDto);

            //Assert
            Assert.IsTrue(result.IsFinished);
            Assert.AreEqual(result.State[0, 0], 1);
            Assert.AreEqual(result.State[1, 1], 1);
            Assert.AreEqual(result.State[2, 2], 1);
        }
    }
}
