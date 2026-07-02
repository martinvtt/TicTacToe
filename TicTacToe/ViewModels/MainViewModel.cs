using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicTacToe.Models;

namespace TicTacToe.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private GameManager _gameManager;

        // ObservableProperty for message status
        [ObservableProperty]
        private string _statusMessage = "Its player's X turn";

        // ObservableProperty for result message
        [ObservableProperty]
        private string _resultMessage = "";

        // ObservableProperty for game
        [ObservableProperty]
        private bool _gameOver = false;

        /**
        The 9 cells of the board, exposed as a collection instead of
        9 separate properties. The XAML iterates over this with an
        ItemsControl, so there's a single button template instead of
        9 near-identical ones.
        **/
        public ObservableCollection<CellViewModel> Cells { get; } = new();

        public MainViewModel()
        {
            _gameManager = new GameManager();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Cells.Add(new CellViewModel(row, col, OnCellClicked));
                }
            }
        }

        // Called by a CellViewModel when its button is clicked.
        private void OnCellClicked(int row, int col)
        {
            if (GameOver)
                return;

            // PlayTurn returns false if the cell is already taken or out of range.
            bool success = _gameManager.PlayTurn(row, col);

            if (!success)
                return;

            UploadGrid();

            GameResult result = _gameManager.CheckResult();

            /**
            Single switch instead of a repetitive if/else chain:
            each branch just picks the message and whether the game ends.
            **/
            switch (result)
            {
                case GameResult.PlayerXWins:
                    ResultMessage = "Player X won !";
                    StatusMessage = "";
                    GameOver = true;
                    break;
                case GameResult.PlayerOWins:
                    ResultMessage = "Player O won !";
                    StatusMessage = "";
                    GameOver = true;
                    break;
                case GameResult.Draw:
                    ResultMessage = "Draw !";
                    StatusMessage = "";
                    GameOver = true;
                    break;
                default:
                    StatusMessage = "Its player's : " + _gameManager.GetBoard().CurrentPlayer + " to play";
                    break;
            }
        }

        // Command new game
        [RelayCommand]
        private void NewGame()
        {
            _gameManager.ResetGame();
            UploadGrid();
            StatusMessage = "Its player's X turn";
            ResultMessage = "";
            GameOver = false;
        }

        /**
        Copies the model's grid into each cell's Symbol property so the
        UI refreshes after every move.
        **/
        private void UploadGrid()
        {
            Board board = _gameManager.GetBoard();

            foreach (CellViewModel cell in Cells)
            {
                cell.Symbol = board.Grid[cell.Row, cell.Col].ToString();
            }
        }
    }
}