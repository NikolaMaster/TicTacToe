using System;

namespace TicTacToe.Dal.Entities
{
    public class Game2Player
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int GameId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime Date { get; set; }

        public Player Player { get; set; }
        public Game Game { get; set; }
    }
}
