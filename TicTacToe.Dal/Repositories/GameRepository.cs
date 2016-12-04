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
    public class GameRepository : IRepository<Game>
    {
        private readonly TicTacToeContext _db;
        private readonly Expression<Func<Game, IEnumerable<Player>>> _includes = g => g.Game2Players.Select(g2P => g2P.Player);

        public GameRepository(TicTacToeContext db)
        {
            _db = db;
        }

        public IEnumerable<Game> GetAll()
        {
            return _db.Games.Include(_includes);
        }

        public Game Get(int id)
        {
            return _db.Games.Find(id);
        }

        public IEnumerable<Game> Find(Expression<Func<Game, bool>> expression)
        {
            return _db.Games.Include(_includes).Where(expression).ToList();
        }

        public void Create(Game item)
        {
            _db.Games.Add(item);
        }

        public void Update(Game item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var game = _db.Games.Find(id);
            if (game != null)
            {
                _db.Games.Remove(game);
            }
        }
    }
}
