using System.Collections.Generic;

namespace TicTacToe.Dal.Entities
{
    public class Game
    {
        public Game()
        {
            Game2Players = new List<Game2Player>();
        }

        public int Id { get; set; }
        public bool IsFinished { get; set; }

        public ICollection<Game2Player> Game2Players { get; set; }
    }
}
