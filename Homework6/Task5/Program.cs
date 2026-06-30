namespace Task5;

class Program
{
    static void Main(string[] args)
    {
        int failCount = 0;

        Result<string> result = RetryExecutor.Execute(
            () =>
            {
                failCount++;

                if (failCount <= 2)
                {
                    throw new Exception($"Failed attempt {failCount}");
                }

                return "Operation completed successfully";
            },
             3);

        Console.WriteLine($"Success:  {result.Success}");
        Console.WriteLine($"Value:    {result.Value}");
        Console.WriteLine($"Attempts: {result.Attempts}");

        if (result.Error != null)
        {
            Console.WriteLine($"Error:    {result.Error.Message}");
        }
    }
}