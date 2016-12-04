namespace TicTacToe.Bll.Dto
{
    public class TurnDto
    {
        public int Id { get; set; }
        public int? GameId { get; set; }
        public int? PlayerId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
