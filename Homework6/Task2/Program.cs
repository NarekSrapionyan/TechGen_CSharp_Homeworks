
namespace Task2;

class Program
{
    static void Main(string[] args)
    {
        int[] arr = { 1, 2, 3, 4, 5 };
    
        Predicate<int> predicate = x => x % 2 == 0;
        Console.WriteLine("Source Numbers: "); 
        Console.WriteLine(string.Join(", ", arr));
        Console.WriteLine("Filtered Numbers: ");
        int[] predicatedNumbers = Filter.FilterArray(arr, predicate);
        Console.WriteLine(string.Join(", ", predicatedNumbers));
        Console.WriteLine("Projected Numbers: ");
        Func<int, string> func = n => $"N{n}";
        string[] projectedNumbers = Project.ProjectValues(predicatedNumbers, func);
        Console.WriteLine(string.Join(", ", projectedNumbers));
        
    }
}