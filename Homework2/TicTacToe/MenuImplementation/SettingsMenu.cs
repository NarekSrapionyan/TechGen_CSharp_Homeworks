using MenuLib;

namespace TicTacToe.MenuImplementation;

public class SettingsMenu : Menu
{
    public SettingsMenu() : base("Settings")
    {
    }

    protected override void InternalDisplay()
    {
        Console.WriteLine($"Current username: {AppData.Username}");
        Console.WriteLine();
        Console.WriteLine("Enter new username:");
    }

    protected override NavigationResult HandleOption(string option)
    {
        if (string.IsNullOrWhiteSpace(option))
        {
            Console.WriteLine("Username cannot be empty.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);

            return NavigationResult.None();
        }

        AppData.Username = option.Trim();

        Console.WriteLine($"Username changed to: {AppData.Username}");
        Console.WriteLine("Press any key to return to main menu...");
        Console.ReadKey(true);

        return NavigationResult.Back();
    }
}