namespace TicTacToe.Game;

public abstract class Player
{
    public string Name { get; }
    public CellSymbol Symbol { get; }

    protected Player(string name, CellSymbol symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}