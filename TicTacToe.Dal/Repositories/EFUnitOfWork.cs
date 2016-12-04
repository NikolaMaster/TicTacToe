using System;
using TicTacToe.Dal.EF;
using TicTacToe.Dal.Entities;
using TicTacToe.Dal.Interfaces;

namespace TicTacToe.Dal.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly TicTacToeContext _db;
        private PlayerRepository _playerRepository;
        private IRepository<Game> _gameRepository;

        public EfUnitOfWork(string connectionString)
        {
            _db = new TicTacToeContext(connectionString);
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _db.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<Player> Players => _playerRepository ?? (_playerRepository = new PlayerRepository(_db));

        public IRepository<Game> Games => _gameRepository ?? (_gameRepository = new GameRepository(_db));

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
