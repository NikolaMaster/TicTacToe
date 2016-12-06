using System.Collections.Generic;
using TicTacToe.Bll.Dto;

namespace TicTacToe.Bll.Interfaces
{
    public interface IPlayerService
    {
        IEnumerable<PlayerDto> GetAll();
        PlayerDto CreatePlayer(PlayerDto playerDto);
        PlayerDto DeletePlayer(int id);
        void Dispose();
    }
}