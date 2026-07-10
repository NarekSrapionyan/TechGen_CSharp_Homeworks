using System.Text;

namespace Task4;

class Program
{
    static void Main(string[] args)
    {
        string logFile = "service.log";

        using (StreamWriter writer = new StreamWriter(logFile, false, Encoding.UTF8))
        {
            writer.WriteLine("Info: Program started");
            writer.WriteLine("Info: User logged in");
            writer.WriteLine("Error: File is missing");
            writer.WriteLine("Info: Программа работает");
            writer.WriteLine("Error: Something went wrong");
        }

        int errorCount = 0;

        using (StreamReader reader = new StreamReader(logFile, Encoding.UTF8))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);

                if (line.Contains("Error:"))
                    errorCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Total error lines: {errorCount}");
    }
}
