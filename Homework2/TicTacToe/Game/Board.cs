namespace TicTacToe.Game;

public class Board
{
    private readonly CellSymbol[] _cells;

    public Board()
    {
        _cells = new CellSymbol[9];
    }

    public CellSymbol GetCell(int index)
    {
        return _cells[index];
    }

    public bool IsCellEmpty(int index)
    {
        return _cells[index] == CellSymbol.Empty;
    }

    public bool PlaceSymbol(int index, CellSymbol symbol)
    {
        if (index < 0 || index >= _cells.Length)
        {
            return false;
        }

        if (!IsCellEmpty(index))
        {
            return false;
        }

        _cells[index] = symbol;

        return true;
    }

    public bool IsFull()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i] == CellSymbol.Empty)
            {
                return false;
            }
        }

        return true;
    }

    public CellSymbol GetWinner()
    {
        int[,] winLines =
        {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },

            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },

            { 0, 4, 8 },
            { 2, 4, 6 }
        };

        for (int i = 0; i < 8; i++)
        {
            int first = winLines[i, 0];
            int second = winLines[i, 1];
            int third = winLines[i, 2];

            if (_cells[first] != CellSymbol.Empty &&
                _cells[first] == _cells[second] &&
                _cells[second] == _cells[third])
            {
                return _cells[first];
            }
        }

        return CellSymbol.Empty;
    }

    public void Draw(int selectedIndex)
    {
        Console.WriteLine();

        for (int row = 0; row < 3; row++)
        {
            DrawCell(row * 3, selectedIndex);
            Console.Write("|");
            DrawCell(row * 3 + 1, selectedIndex);
            Console.Write("|");
            DrawCell(row * 3 + 2, selectedIndex);

            Console.WriteLine();

            if (row < 2)
            {
                Console.WriteLine("---+---+---");
            }
        }

        Console.WriteLine();
    }

    private void DrawCell(int index, int selectedIndex)
    {
        bool isSelected = index == selectedIndex;

        if (isSelected)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
        }

        Console.Write($" {GetCellText(index)} ");

        if (isSelected)
        {
            Console.ResetColor();
        }
    }

    private string GetCellText(int index)
    {
        if (_cells[index] == CellSymbol.X)
        {
            return "X";
        }

        if (_cells[index] == CellSymbol.O)
        {
            return "O";
        }

        return (index + 1).ToString();
    }
}