using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Web.Model
{
    public class UserTurnDto
    {
        public int? GameId { get; set; }
        [Required]
        public int? PlayerId { get; set; }
        [Required]
        public int? X { get; set; }
        [Required]
        public int? Y { get; set; }
    }
}