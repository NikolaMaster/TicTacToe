using TicTacToe.Bll.Dto;
using TicTacToe.Bll.Interfaces;
using TicTacToe.Dal.Entities;
using TicTacToe.Dal.Interfaces;

namespace TicTacToe.Bll.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _database;

        public PlayerService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public void CreatePlayer(PlayerDto playerDto)
        {
            _database.Players.Create(new Player
            {
                Name = playerDto.Name
            });

            _database.Save();
        }

        public void DeletePlayer(int id)
        {
            _database.Players.Delete(id);
            _database.Save();
        }
    }
}
