namespace TicTacToe.Bll.Dto
{
    public class GameDto
    {
        public GameDto(int capacity)
        {
            State = new byte[capacity, capacity];
        }

        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public byte[,] State { get; set; }
    }
}
