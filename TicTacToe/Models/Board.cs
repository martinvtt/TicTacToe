namespace TicTacToe.Models
{
    /**
    Holds the 3x3 grid state and whose turn it is.
     ' ' means an empty cell, 'X' and 'O' mark played moves.
     **/
    public class Board
    {
        public char[,] Grid { get; set; }
        public char CurrentPlayer { get; set; }

        public Board()
        {
            Grid = new char[3, 3];
            CurrentPlayer = 'X'; // X always starts

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Grid[i, j] = ' ';
                }
            }
        }
    }
}