using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using TicTacToe.Bll.Dto;
using TicTacToe.Bll.Infrastructure;
using TicTacToe.Bll.Interfaces;

namespace TicTacToe.Web.Controllers
{
    public class PlayersController : ApiController
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public IEnumerable<PlayerDto> Get()
        {
            return _playerService.GetAll();
        }

        [ResponseType(typeof (PlayerDto))]
        public IHttpActionResult Post(string name)
        {
            try
            {
                return Ok(_playerService.CreatePlayer(new PlayerDto
                {
                    Name = name
                }));
            }
            catch (CustomValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof (PlayerDto))]
        public IHttpActionResult Delete(int id)
        {
            var res = _playerService.DeletePlayer(id);
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}