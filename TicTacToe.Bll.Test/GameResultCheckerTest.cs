using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Bll.BusinessModel;

namespace TicTacToe.Bll.Test
{
    [TestClass]
    public class GameResultCheckerTest
    {
        [TestMethod]
        public void TestDoGameFinishedNotFinished()
        {
            var checker = new GameResultChecker();

            var field = new sbyte[,]
            {
                {  0, -1, -1 },
                { -1,  0, -1 },
                { -1, -1,  0 }
            };

            var result = checker.DoesGameFinished(field);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestDoGameFinishedLeftDiagonalFinished()
        {
            var checker = new GameResultChecker();

            var field = new sbyte[,]
            {
                { 1,-1,-1 },
                { -1,1,-1 },
                { -1,-1,1 }
            };

            var result = checker.DoesGameFinished(field);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestDoGameFinishedRightDiagonalFinished()
        {
            var checker = new GameResultChecker();

            var field = new sbyte[,]
            {
                { -1,-1,1 },
                { -1,1,-1 },
                { 1,-1,-1 }
            };

            var result = checker.DoesGameFinished(field);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestDoGameFinishedHorizontalFinished()
        {
            var checker = new GameResultChecker();

            var field = new sbyte[,]
            {
                { 1,  1,  1},
                {-1, -1, -1},
                { 1,  0,  0}
            };

            var result = checker.DoesGameFinished(field);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestDoGameFinishedVerticalFinished()
        {
            var checker = new GameResultChecker();

            var field = new sbyte[,]
            {
                { 1,-1, 1 },
                { 1,-1,-1 },
                { 1,-1,-1 }
            };

            var result = checker.DoesGameFinished(field);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestDoGameNotFinished()
        {
            var checker = new GameResultChecker();

            var field = new sbyte[,]
            {
                { 1, 0, 1 },
                { 0, 0, -1 },
                { 1, 0, -1 }
            };

            var result = checker.DoesGameFinished(field);

            Assert.IsFalse(result);
        }
    }
}
