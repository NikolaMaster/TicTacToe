using System.Data.Entity;
using TicTacToe.Dal.Entities;

namespace TicTacToe.Dal.EF
{
    public class TicTacToeContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; } 
        public DbSet<Game2Player> Game2Players { get; set; }

        public TicTacToeContext(string connectionString) : base(connectionString)
        {
        }
    }
}
