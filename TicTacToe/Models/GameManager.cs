using TicTacToe.Models;

namespace TicTacToe.Models
{
    // Handles game state and rules on top of a Board instance.
    public class GameManager
    {
        private Board _board;

        public GameManager()
        {
            _board = new Board();
        }

        public Board GetBoard()
        {
            return _board;
        }

        public void ResetGame()
        {
            _board = new Board();
        }

        // A move is valid if it's inside the grid and the cell is empty.
        public bool IsValidMove(int row, int col)
        {
            if (row < 0 || row > 2 || col < 0 || col > 2)
                return false;

            return _board.Grid[row, col] == ' ';
        }

        public bool PlayTurn(int row, int col)
        {
            if (!IsValidMove(row, col))
                return false;

            _board.Grid[row, col] = _board.CurrentPlayer;
            _board.CurrentPlayer = _board.CurrentPlayer == 'X' ? 'O' : 'X';

            return true;
        }

        /**
        Checks all 8 possible winning lines (3 rows, 3 columns, 2 diagonals)
        by reading directly from the grid instead of building temporary
        arrays with LINQ, so no extra allocations happen on every move.
        **/
        public GameResult CheckResult()
        {
            for (int i = 0; i < 3; i++)
            {
                GameResult? rowResult = CheckLine(
                    _board.Grid[i, 0], _board.Grid[i, 1], _board.Grid[i, 2]);
                if (rowResult != null) return rowResult.Value;

                GameResult? colResult = CheckLine(
                    _board.Grid[0, i], _board.Grid[1, i], _board.Grid[2, i]);
                if (colResult != null) return colResult.Value;
            }

            GameResult? diag1 = CheckLine(
                _board.Grid[0, 0], _board.Grid[1, 1], _board.Grid[2, 2]);
            if (diag1 != null) return diag1.Value;

            GameResult? diag2 = CheckLine(
                _board.Grid[0, 2], _board.Grid[1, 1], _board.Grid[2, 0]);
            if (diag2 != null) return diag2.Value;

            return IsBoardFull() ? GameResult.Draw : GameResult.InProgress;
        }

        /**
        Returns a win result if the three cells match and aren't empty,
        otherwise null. Shared by row, column and diagonal checks.
        **/
        private static GameResult? CheckLine(char a, char b, char c)
        {
            if (a == ' ' || a != b || b != c)
                return null;

            return a == 'X' ? GameResult.PlayerXWins : GameResult.PlayerOWins;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (_board.Grid[i, j] == ' ')
                        return false;

            return true;
        }
    }
}