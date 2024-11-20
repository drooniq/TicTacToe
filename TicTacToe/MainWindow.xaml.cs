using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameEngine gameEngine;

        public MainWindow()
        {
            InitializeComponent();
            gameEngine = new GameEngine();
            UpdateStatisticsUI();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                int row = Grid.GetRow(btn);
                int column = Grid.GetColumn(btn);
                short index = (short)(row * 3 + column);

                if (gameEngine.MarkBoard(index, gameEngine.CurrentPlayer))
                {
                    btn.Content = (gameEngine.CurrentPlayer == Player.Human) ? "X" : "O";

                    GameState gameState = gameEngine.CheckForWinner();
                    if (gameState != GameState.OnGoing)
                    {
                        if (gameState == GameState.Draw)
                            WinnerBtn.Content = "There was a Draw!";
                        else
                            WinnerBtn.Content = (gameEngine.CurrentPlayer == Player.Human) ? "Human Wins!" : "Computer Wins!";
                        WinnerBtn.Visibility = Visibility.Visible;
                        DisableAllGameButtons();
                    }
                    else
                    {
                        gameEngine.ChangePlayer();

                        if (gameEngine.CurrentPlayer == Player.Computer)
                        {
                            var NewMoveIndex = gameEngine.MakeComputerMove();
                            var indexButton = GetIndexButton(NewMoveIndex);
                            Button_Click(indexButton, e);
                        }
                    }

                }
            }
            UpdateStatisticsUI();
        }

        private void UpdateStatisticsUI()
        {
            GameStatistics gameStatistics = gameEngine.gameStatistics;
            StatUI.Text = $"Human: {gameStatistics.HumanWins}  " +
                          $"Computer: {gameStatistics.ComputerWins}\n" +
                          $"Draws: {gameStatistics.Draws}  " +
                          $"Total Games: {gameStatistics.TotalGames}";
        }

        private Button GetIndexButton(short index)
        {
            return MainGrid.Children[index] as Button;
        }

        private void DisableAllGameButtons()
        {
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Button btn)
                {
                    btn.IsEnabled = false;
                }
            }
        }
        private void EnableAllGameButtons()
        {
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Button btn)
                {
                    btn.IsEnabled = true;
                }
            }
        }
        private void ClearAllUIButtons()
        {
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Button btn)
                {
                    btn.Content = "";
                }
            }
        }

        private void StartNewGame(object sender, RoutedEventArgs e)
        {
            EnableAllGameButtons();
            ClearAllUIButtons();
            WinnerBtn.Visibility = Visibility.Hidden;
            gameEngine.Reset();
            UpdateStatisticsUI();
        }
    }
}