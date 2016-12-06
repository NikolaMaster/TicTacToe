using System.Collections.Generic;
using System.Linq;
using TicTacToe.Bll.Dto;
using TicTacToe.Bll.Infrastructure;
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

        public IEnumerable<PlayerDto> GetAll()
        {
            return _database.Players.GetAll().Select(convertToDto);
        }

        public PlayerDto CreatePlayer(PlayerDto playerDto)
        {
            var playerDb = _database.Players.Find(p => string.Equals(p.Name, playerDto.Name)).FirstOrDefault();
            if (playerDb != null)
            {
                throw new CustomValidationException("Player with such name is already exists", "Name");
            }

            var player = new Player
            {
                Name = playerDto.Name
            };

            _database.Players.Create(player);
            _database.Save();
            return convertToDto(player);
        }

        public PlayerDto DeletePlayer(int id)
        {
            var deletedPlayer = _database.Players.Get(id);            
            if (deletedPlayer == null)
            {
                return null;
            }

            _database.Players.Delete(id);
            _database.Save();
            return convertToDto(deletedPlayer);
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        private static PlayerDto convertToDto(Player player)
        {
            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name
            };
        }
    }
}
