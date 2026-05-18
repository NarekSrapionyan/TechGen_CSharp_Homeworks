using MenuLib;

namespace TicTacToe.MenuImplementation;

public class AboutMenu : Menu
{
    public AboutMenu() : base("About")
    {
    }

    protected override void InternalDisplay()
    {
        Console.WriteLine("Developer: Write your name here");
        Console.WriteLine("Course: C#");
        Console.WriteLine("Year: 2026");
        Console.WriteLine();
        Console.WriteLine("Type 'back' to return to main menu.");
    }

    protected override NavigationResult HandleOption(string option)
    {
        return NavigationResult.None();
    }
}