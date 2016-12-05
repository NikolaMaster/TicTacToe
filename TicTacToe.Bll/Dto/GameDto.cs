namespace TicTacToe.Bll.Dto
{
    public class GameDto
    {
        public GameDto(int capacity)
        {
            State = new sbyte[capacity, capacity];
        }

        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public sbyte[,] State { get; set; }
    }
}
