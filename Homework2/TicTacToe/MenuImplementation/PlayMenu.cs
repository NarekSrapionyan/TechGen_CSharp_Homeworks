using MenuLib;
using TicTacToe.Game;

namespace TicTacToe.MenuImplementation;

public class PlayMenu : Menu
{
    public PlayMenu() : base("Gameplay Mode Selection")
    {
        ConfigureOptionSize(2);

        AddOption("1", "Player vs Player");
        AddOption("2", "Player vs Computer");
    }

    protected override NavigationResult HandleOption(string option)
    {
        GameMode mode;

        switch (option)
        {
            case "1":
                mode = GameMode.PlayerVsPlayer;
                break;

            case "2":
                mode = GameMode.PlayerVsComputer;
                break;

            default:
                return NavigationResult.None();
        }

        CellSymbol selectedSymbol = AskSymbol();

        if (selectedSymbol == CellSymbol.Empty)
        {
            return NavigationResult.Back();
        }

        TicTacToeGame game = new TicTacToeGame(mode, selectedSymbol);
        game.Start();

        return NavigationResult.Home();
    }

    private CellSymbol AskSymbol()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Choose your symbol:");
            Console.WriteLine("X - play as X");
            Console.WriteLine("O - play as O");
            Console.WriteLine();
            Console.WriteLine("Type 'back' to return.");
            Console.WriteLine();

            Console.Write("Your choice: ");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            input = input.Trim().ToUpper();

            if (input == "X")
            {
                return CellSymbol.X;
            }

            if (input == "O" || input == "0")
            {
                return CellSymbol.O;
            }

            if (input == "BACK")
            {
                return CellSymbol.Empty;
            }

            Console.WriteLine("Invalid symbol. Please enter X or O.");
            Console.WriteLine("Press any key to try again...");
            Console.ReadKey(true);
        }
    }
}