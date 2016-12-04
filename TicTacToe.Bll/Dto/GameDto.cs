namespace TicTacToe.Bll.Dto
{
    public class GameDto
    {
        public GameDto(int capacity)
        {
            State = new int?[capacity, capacity];
        }

        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public int?[,] State { get; set; }
    }
}
