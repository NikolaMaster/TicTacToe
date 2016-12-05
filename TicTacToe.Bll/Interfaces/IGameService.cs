using System.Collections.Generic;
using TicTacToe.Bll.Dto;

namespace TicTacToe.Bll.Interfaces
{
    public interface IGameService
    {
        GameDto MakeAiTurn(int id);
        GameDto MakeTurn(TurnDto turnDto);
        GameDto GetGame(int id);
        IEnumerable<GameDto> GetGames();
        GameDto DeleteGame(int id);
        void Dispose();
    }
}