using System.Collections.Generic;
using System.Web.Http;
using TicTacToe.Bll.Dto;
using TicTacToe.Bll.Infrastructure;
using TicTacToe.Bll.Interfaces;
using TicTacToe.Web.Model;

namespace TicTacToe.Web.Controllers
{
    [RoutePrefix("Games")]
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IEnumerable<GameDto> Get()
        {
            return _gameService.GetGames();
        }

        public IHttpActionResult Get(int id)
        {
            var foundGame = _gameService.GetGame(id);
            if (foundGame == null)
            {
                return NotFound();
            }

            return Ok(foundGame);
        }

        [Route("Turns/Player")]
        public IHttpActionResult PostUserTurn(UserTurnDto playersTurn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var turnDto = new TurnDto
                {
                    GameId = playersTurn.GameId,
                    PlayerId = playersTurn.PlayerId,
                    X = playersTurn.X ?? 0,
                    Y = playersTurn.Y ?? 0
                };

                var res = _gameService.MakeTurn(turnDto);
                return Ok(res);
            }
            catch (CustomValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
        }

        [Route("Turns/AI")]
        public IHttpActionResult PostAiTurn(int gameId)
        {
            try
            {
                var game = _gameService.GetGame(gameId);
                if (game == null)
                {
                    return NotFound();
                }

                var res = _gameService.MakeAiTurn(gameId);
                return Ok(res);
            }
            catch (CustomValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            var res = _gameService.DeleteGame(id);
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}