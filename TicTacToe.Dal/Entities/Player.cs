using System.Collections.Generic;

namespace TicTacToe.Dal.Entities
{
    public class Player
    {
        public Player()
        {
            Game2Players = new List<Game2Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Game2Player> Game2Players { get; set; } 
    }
}
