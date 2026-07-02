using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TicTacToe.ViewModels
{
    /**
    Represents a single cell on the board. It knows its own position and
    delegates the click to a callback given by MainViewModel, so we don't
    need one property + one hardcoded button per cell anymore.
    **/
    public partial class CellViewModel : ObservableObject
    {
        public int Row { get; }
        public int Col { get; }

        [ObservableProperty]
        private string _symbol = " ";

        private readonly Action<int, int> _onClicked;

        public CellViewModel(int row, int col, Action<int, int> onClicked)
        {
            Row = row;
            Col = col;
            _onClicked = onClicked;
        }

        [RelayCommand]
        private void Click() => _onClicked(Row, Col);
    }
}