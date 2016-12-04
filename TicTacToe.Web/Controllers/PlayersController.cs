using System.Collections.Generic;
using System.Web.Http;

namespace TicTacToe.Web.Controllers
{
    public class PlayersController : ApiController
    {
        // GET api/players
        public IEnumerable<Players> Get()
        {
            return new string[] { "value1", "value2" };
        }       

        // POST api/players
        public void Post([FromBody]string value)
        {
        }

        // PUT api/players/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/players/5
        public void Delete(int id)
        {
        }
    }
}
