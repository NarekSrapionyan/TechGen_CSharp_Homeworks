namespace MenuLib;

public static class MenuRunner
{
    private static readonly MenuStack NavigationStack = new MenuStack();

    public static void Run(Menu root)
    {
        NavigationStack.Push(root);

        while (NavigationStack.Count > 0)
        {
            Menu currentMenu = NavigationStack.Peek();

            currentMenu.Display();

            Console.WriteLine();
            Console.Write("Select option: ");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            NavigationResult result = currentMenu.ExecuteOption(input.Trim());

            switch (result.Type)
            {
                case NavigationResultType.None:
                    break;

                case NavigationResultType.GoTo:
                    if (result.Menu != null)
                    {
                        NavigationStack.Push(result.Menu);
                    }
                    break;

                case NavigationResultType.Wait:
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    break;

                case NavigationResultType.Back:
                    if (NavigationStack.Count > 1)
                    {
                        NavigationStack.Pop();
                    }
                    break;

                case NavigationResultType.Home:
                    while (NavigationStack.Count > 1)
                    {
                        NavigationStack.Pop();
                    }
                    break;

                case NavigationResultType.Exit:
                    return;
            }
        }
    }
}