using MenuLib;
using TicTacToe;
using TicTacToe.MenuImplementation;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Welcome to Tic Tac Toe!");
Console.WriteLine();

while (string.IsNullOrWhiteSpace(AppData.Username))
{
    Console.Write("Enter username: ");

    AppData.Username = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrWhiteSpace(AppData.Username))
    {
        Console.WriteLine("Username cannot be empty.");
        Console.WriteLine();
    }
}

MenuRunner.Run(new MainMenu());