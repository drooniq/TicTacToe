namespace TicTacToe
{
    public class GameStatistics
    {
        public int HumanWins { get; set; } = 0;
        public int ComputerWins { get; set; } = 0;
        public int Draws { get; set; } = 0;
        public int NumberOfMoves { get; set; } = 0;

        public int TotalGames => HumanWins + ComputerWins + Draws;

        public GameStatistics()
        {
        }
    }
}
