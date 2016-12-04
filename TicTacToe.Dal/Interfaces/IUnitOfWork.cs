using System;
using TicTacToe.Dal.Entities;

namespace TicTacToe.Dal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Player> Players { get; }
        IRepository<Game> Games { get; }
        void Save();
    }
}