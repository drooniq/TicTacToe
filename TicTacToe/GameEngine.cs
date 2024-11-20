namespace TicTacToe
{
    public enum WinnerRow
    { 
        None,
        Row0,
        Row1,
        Row2
    }

    public enum WinnerColumn
    {
        None,
        Column0,
        Column1,
        Column2
    }

    public enum WinnerDiagonal
    {
        None,
        Diagonal1,  // 0,0 - 2,2
        Diagonal2   // 2,0 - 0,2
    }

    public enum Player
    {
        Human = -1,
        None = 0,
        Computer = 2
    }

    public enum GameState
    {
        OnGoing,
        Draw,
        HumanWin,
        ComputerWin
    }

    public class GameEngine
    {
        // one dimensional array to represent a 3x3 board. 0 = upper left corner, 9 = lower right corner
        private short[] board = new short[3 * 3];

        public GameStatistics gameStatistics = new GameStatistics();

        public Player CurrentPlayer = Player.Human;

        public GameEngine()
        {
            Reset();
        }

        public void Reset()
        {
            Array.Clear(board, 0, board.Length);
            gameStatistics.NumberOfMoves = 0;
        }

        public void ChangePlayer()
        {
            CurrentPlayer = (CurrentPlayer == Player.Human) ? Player.Computer : Player.Human;
        }

        private short[] GetAvailableBoardCells()
        {
            List<short> availableCells = new List<short>();
            for (short i = 0; i < board.Length; i++)
            {
                if (board[i] == (short)Player.None)
                    availableCells.Add(i);
            }
            return availableCells.ToArray();
        }

        private short FindBestMove()
        {
            short[] availableCells = GetAvailableBoardCells();
            short BestMove = -1;

            short rnd = (short)new Random().Next(0, availableCells.Length);
            BestMove = availableCells[rnd];

            return BestMove;
        }

        public short MakeComputerMove()
        {
            short bestMove = FindBestMove();
            //MarkBoard(bestMove, Player.Computer);

            return bestMove;
        }

        public bool MarkBoard(short GameBoardIndex, Player player)
        {
            if (gameStatistics.NumberOfMoves == 9)
                return false;

            if (board[GameBoardIndex] != (short)Player.None)
                return false;
            
            board[GameBoardIndex] = (short)player;

            gameStatistics.NumberOfMoves++;
            return true;
        }

        public GameState CheckForWinner()
        {
            if (gameStatistics.NumberOfMoves < 5)
                return GameState.OnGoing;

            GameState gameState = (CurrentPlayer == Player.Human) ? GameState.HumanWin : GameState.ComputerWin;

            if (CheckRows() != WinnerRow.None)
            {
                if (gameState == GameState.HumanWin)
                    gameStatistics.HumanWins++;
                else
                    gameStatistics.ComputerWins++;
                return gameState;
            }
            if (CheckColumns() != WinnerColumn.None)
            {
                if (gameState == GameState.HumanWin)
                    gameStatistics.HumanWins++;
                else
                    gameStatistics.ComputerWins++;
                return gameState;
            }
            if (CheckDiagonals() != WinnerDiagonal.None)
            {
                if (gameState == GameState.HumanWin)
                    gameStatistics.HumanWins++;
                else
                    gameStatistics.ComputerWins++;
                return gameState;
            }
            if (CheckDraw())
                return GameState.Draw;

            return GameState.OnGoing;
        }

        private bool CheckDraw()
        {
            if (gameStatistics.NumberOfMoves == 9)
            {
                gameStatistics.Draws++;
                return true;
            }
            return false;
        }

        public WinnerRow CheckRows()
        {
            if ((board[0] != 0 && board[0] == board[1]) && (board[1] == board[2]))
                return WinnerRow.Row0;

            if ((board[3] != 0 && board[3] == board[4]) && (board[4] == board[5]))
                return WinnerRow.Row1;

            if ((board[6] != 0 && board[6] == board[7]) && (board[7] == board[8]))
                return WinnerRow.Row2;

            return WinnerRow.None;
        }

        public WinnerColumn CheckColumns()
        {
            if ((board[0] != 0 && board[0] == board[3]) && (board[3] == board[6]))
                return WinnerColumn.Column0;

            if ((board[1] != 0 && board[1] == board[4]) && (board[4] == board[7]))
                return WinnerColumn.Column1;

            if ((board[2] != 0 && board[2] == board[5]) && (board[5] == board[8]))
                return WinnerColumn.Column2;

            return WinnerColumn.None;
        }

        public WinnerDiagonal CheckDiagonals()
        {
            if ((board[0] != 0 && board[0] == board[4]) && (board[4] == board[8]))
                return WinnerDiagonal.Diagonal1;

            if ((board[2] != 0 && board[2] == board[4]) && (board[4] == board[6]))
                return WinnerDiagonal.Diagonal2;

            return WinnerDiagonal.None;
        }
    }
}
