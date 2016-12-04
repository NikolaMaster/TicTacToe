using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TicTacToe.Dal.EF;
using TicTacToe.Dal.Entities;
using TicTacToe.Dal.Interfaces;

namespace TicTacToe.Dal.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        private readonly TicTacToeContext _db;

        public PlayerRepository(TicTacToeContext db)
        {
            _db = db;
        }

        public IEnumerable<Player> GetAll()
        {
            return _db.Players;
        }

        public Player Get(int id)
        {
            return _db.Players.Find(id);
        }

        public IEnumerable<Player> Find(Expression<Func<Player, bool>> predicate)
        {
            return _db.Players.Where(predicate);
        }

        public void Create(Player item)
        {
            _db.Players.Add(item);
        }

        public void Update(Player item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var player = _db.Players.Find(id);
            if (player != null)
            {
                _db.Players.Remove(player);
            }
        }
    }
}
