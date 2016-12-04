namespace TicTacToe.Bll.Dto
{
    public class GameDto
    {
        public GameDto(int capacity)
        {
            State = new string[capacity, capacity];
        }

        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public string[,] State { get; set; }
    }
}
