namespace TicTacToe.Bll.BusinessModel
{
    public class GameResultChecker
    {
        private int _gameCapacity;

        public bool DoesGameFinished(byte[,] matrix)
        {
            _gameCapacity = matrix.GetLength(0);
            for (var i = 0; i < _gameCapacity; i++)
            {
                var rowSum = 0;
                var columnSum = 0;
                for (var j = _gameCapacity; j > 0; j--)
                {
                    rowSum += matrix[i, _gameCapacity - j];
                    columnSum += matrix[_gameCapacity - j, i];
                }

                if (!checkLine(rowSum) && !checkLine(columnSum))
                {
                    continue;
                }

                return true;
            }

            var diag1Sum = 0;
            for (var i = 0; i < _gameCapacity; i++)
            {
                diag1Sum += matrix[i, i];
            }

            var diag2Sum = 0;
            for (var i = 0; i < _gameCapacity; i++)
            {
                diag2Sum += matrix[i, _gameCapacity - 1 - i];
            }

            return checkLine(diag1Sum) || checkLine(diag2Sum);
        }

        private bool checkLine(int lineSum)
        {
            return lineSum == _gameCapacity || lineSum == _gameCapacity*2;
        }
    }
}
