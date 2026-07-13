namespace CopyProject;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Options options = ArgumentParser.Parse(args);

            ArgumentValidator.Validate(options);

            Console.WriteLine("Arguments are valid.");

            Copy.Start(options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}