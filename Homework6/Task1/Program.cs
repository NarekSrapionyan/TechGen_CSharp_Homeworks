namespace Task1;

class Program
{
    static void Main(string[] args)
    {
        Pair<int,string> pair1 = new Pair<int,string>(1, "One");
        Pair<string, int> swapped = pair1.SwapSides();
        Console.WriteLine($"Original Pair : {pair1}");
        Console.WriteLine($"Swapped Pair  : {swapped}");
        
    }
}
