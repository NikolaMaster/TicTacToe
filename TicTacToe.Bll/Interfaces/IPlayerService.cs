using TicTacToe.Bll.Dto;

namespace TicTacToe.Bll.Interfaces
{
    public interface IPlayerService
    {
        void CreatePlayer(PlayerDto playerDto);
        void DeletePlayer(int id);
    }
}