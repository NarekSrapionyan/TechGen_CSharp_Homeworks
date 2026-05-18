namespace TicTacToe.Game;

public class TicTacToeGame
{
    private readonly Board _board;
    private readonly GameMode _mode;

    private readonly HumanPlayer _player1;
    private readonly Player _player2;

    private CellSymbol _currentTurn;
    private int _selectedIndex;

    public TicTacToeGame(GameMode mode, CellSymbol firstPlayerSymbol)
    {
        _board = new Board();
        _mode = mode;

        CellSymbol secondPlayerSymbol = GetOppositeSymbol(firstPlayerSymbol);

        _player1 = new HumanPlayer(AppData.Username, firstPlayerSymbol);

        if (mode == GameMode.PlayerVsComputer)
        {
            _player2 = new ComputerPlayer(secondPlayerSymbol);
        }
        else
        {
            _player2 = new HumanPlayer("Player 2", secondPlayerSymbol);
        }

        _currentTurn = CellSymbol.X;
        _selectedIndex = 4;
    }

    public void Start()
    {
        while (true)
        {
            Draw();

            if (_mode == GameMode.PlayerVsComputer && _currentTurn == _player2.Symbol)
            {
                MakeComputerMove();

                if (IsGameFinished())
                {
                    return;
                }

                SwitchTurn();
                continue;
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }

            if (IsMoveKey(key))
            {
                MoveSelection(key);
                continue;
            }

            if (key.Key == ConsoleKey.Enter)
            {
                bool placed = _board.PlaceSymbol(_selectedIndex, _currentTurn);

                if (!placed)
                {
                    continue;
                }

                if (IsGameFinished())
                {
                    return;
                }

                SwitchTurn();
            }
        }
    }

    private void Draw()
    {
        Console.Clear();

        Console.WriteLine($"{_player1.Name} ({GetSymbolText(_player1.Symbol)}) vs {_player2.Name} ({GetSymbolText(_player2.Symbol)})");
        Console.WriteLine();

        _board.Draw(_selectedIndex);

        Player currentPlayer = GetCurrentPlayer();

        Console.WriteLine($"Current Turn: {currentPlayer.Name} ({GetSymbolText(currentPlayer.Symbol)})");
        Console.WriteLine();
        Console.WriteLine("Use Arrow Keys or WASD to navigate.");
        Console.WriteLine("Press Enter to place symbol.");
        Console.WriteLine("Press Esc to return to main menu.");
    }

    private void MakeComputerMove()
    {
        ComputerPlayer computer = (ComputerPlayer)_player2;

        int move = computer.GetMove(_board, _player1.Symbol);

        if (move != -1)
        {
            Thread.Sleep(400);
            _board.PlaceSymbol(move, computer.Symbol);
        }
    }

    private bool IsGameFinished()
    {
        CellSymbol winner = _board.GetWinner();

        if (winner != CellSymbol.Empty)
        {
            Draw();

            Player winnerPlayer = GetPlayerBySymbol(winner);

            Console.WriteLine();
            Console.WriteLine($"Winner: {winnerPlayer.Name} ({GetSymbolText(winnerPlayer.Symbol)})");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey(true);

            return true;
        }

        if (_board.IsFull())
        {
            Draw();

            Console.WriteLine();
            Console.WriteLine("Draw!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey(true);

            return true;
        }

        return false;
    }

    private bool IsMoveKey(ConsoleKeyInfo key)
    {
        return key.Key == ConsoleKey.LeftArrow ||
               key.Key == ConsoleKey.RightArrow ||
               key.Key == ConsoleKey.UpArrow ||
               key.Key == ConsoleKey.DownArrow ||
               key.Key == ConsoleKey.A ||
               key.Key == ConsoleKey.D ||
               key.Key == ConsoleKey.W ||
               key.Key == ConsoleKey.S;
    }

    private void MoveSelection(ConsoleKeyInfo key)
    {
        int row = _selectedIndex / 3;
        int column = _selectedIndex % 3;

        if ((key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A) && column > 0)
        {
            column--;
        }
        else if ((key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D) && column < 2)
        {
            column++;
        }
        else if ((key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W) && row > 0)
        {
            row--;
        }
        else if ((key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S) && row < 2)
        {
            row++;
        }

        _selectedIndex = row * 3 + column;
    }

    private void SwitchTurn()
    {
        if (_currentTurn == CellSymbol.X)
        {
            _currentTurn = CellSymbol.O;
        }
        else
        {
            _currentTurn = CellSymbol.X;
        }
    }

    private Player GetCurrentPlayer()
    {
        if (_player1.Symbol == _currentTurn)
        {
            return _player1;
        }

        return _player2;
    }

    private Player GetPlayerBySymbol(CellSymbol symbol)
    {
        if (_player1.Symbol == symbol)
        {
            return _player1;
        }

        return _player2;
    }

    private CellSymbol GetOppositeSymbol(CellSymbol symbol)
    {
        if (symbol == CellSymbol.X)
        {
            return CellSymbol.O;
        }

        return CellSymbol.X;
    }

    private string GetSymbolText(CellSymbol symbol)
    {
        if (symbol == CellSymbol.X)
        {
            return "X";
        }

        if (symbol == CellSymbol.O)
        {
            return "O";
        }

        return "";
    }
}