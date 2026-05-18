namespace MenuLib;

public struct Option
{
    public string Key { get; }
    public string Value { get; }

    public Option(string key, string value)
    {
        Key = key;
        Value = value;
    }
}

public abstract class Menu
{
    public string Title { get; }

    private Option[] _options;
    private int _optionIndex;

    protected Menu(string title)
    {
        Title = title;
        _options = new Option[0];
        _optionIndex = 0;
    }

    protected void ConfigureOptionSize(int count)
    {
        if (count < 0)
        {
            count = 0;
        }

        _options = new Option[count];
        _optionIndex = 0;
    }

    protected void AddOption(string key, string value)
    {
        if (_optionIndex == _options.Length)
        {
            ResizeOptions();
        }

        _options[_optionIndex] = new Option(key, value);
        _optionIndex++;
    }

    private void ResizeOptions()
    {
        int newSize = _options.Length == 0 ? 2 : _options.Length * 2;
        Option[] newOptions = new Option[newSize];

        for (int i = 0; i < _optionIndex; i++)
        {
            newOptions[i] = _options[i];
        }

        _options = newOptions;
    }

    private bool ContainsOption(string key)
    {
        for (int i = 0; i < _optionIndex; i++)
        {
            if (_options[i].Key == key)
            {
                return true;
            }
        }

        return false;
    }

    public void Display()
    {
        Console.Clear();

        Console.WriteLine($"=== {Title} ===");
        Console.WriteLine();

        for (int i = 0; i < _optionIndex; i++)
        {
            Console.WriteLine($"{_options[i].Key} - {_options[i].Value}");
        }

        if (_optionIndex > 0)
        {
            Console.WriteLine();
        }

        InternalDisplay();

        Console.WriteLine();
        Console.WriteLine("--- Navigation ---");
        Console.WriteLine("Type 'back' to go back.");
        Console.WriteLine("Type 'exit' to exit.");
    }

    protected virtual void InternalDisplay()
    {
    }

    public NavigationResult ExecuteOption(string option)
    {
        if (option == "back")
        {
            return NavigationResult.Back();
        }

        if (option == "exit")
        {
            return NavigationResult.Exit();
        }

        if (_optionIndex == 0 || ContainsOption(option))
        {
            Console.Clear();
            return HandleOption(option);
        }

        Console.WriteLine("Invalid option.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);

        return NavigationResult.None();
    }

    protected abstract NavigationResult HandleOption(string option);
}