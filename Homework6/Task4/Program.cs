namespace Task4;

class Program
{
    static void Main(string[] args)
    {
        int[] values = { 5, 1, 9, 3, 7, 2 };

        TopBuffer<int> buffer = new TopBuffer<int>(3);

        Console.WriteLine("Source values:");
        Console.WriteLine(string.Join(", ", values));
        Console.WriteLine();

        foreach (int value in values)
        {
            buffer.Add(value);

            Console.WriteLine($"Added {value} -> [{string.Join(", ", buffer.CopyArray())}]");
        }

        Console.WriteLine();
        Console.WriteLine("Final Top 3:");
        Console.WriteLine(string.Join(", ", buffer.CopyArray()));
    }
}