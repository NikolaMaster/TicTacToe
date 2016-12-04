using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Bll.BusinessModel
{
    public class GameResultChecker
    {
        private int _gameCapacity;

        public bool DoesGameFinished(int?[,] matrix)
        {
            _gameCapacity = matrix.GetLength(0);
            for (var i = 0; i < _gameCapacity; i++)
            {
                var row = new List<int>();
                var column = new List<int>();
                for (var j = _gameCapacity; j > 0; j--)
                {
                    row.Add(getMatrixValue(matrix[i, _gameCapacity - j]));
                    column.Add(getMatrixValue(matrix[_gameCapacity - j, i]));
                }

                if (!checkLine(row) && !checkLine(column))
                {
                    continue;
                }

                return true;
            }

            var diag1 = new List<int>();
            for (var i = 0; i < _gameCapacity; i++)
            {
                diag1.Add(getMatrixValue(matrix[i, i]));
            }

            var diag2 = new List<int>();
            for (var i = 0; i < _gameCapacity; i++)
            {
                diag2.Add(getMatrixValue(matrix[i, _gameCapacity - 1 - i]));
            }

            return checkLine(diag1) || checkLine(diag2);
        }

        private static int getMatrixValue(int? value)
        {
            return value ?? -1;
        }

        private bool checkLine(IEnumerable<int> line)
        {
            var sum = line.Sum();
            return sum == _gameCapacity || sum == 0;
        }
    }
}
