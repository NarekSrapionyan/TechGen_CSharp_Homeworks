namespace Task3;

class Program
{
    static void Main(string[] args)
    {
        TestEntity obj = Initializer.CreateAndInitialize<TestEntity>();
        Console.WriteLine("Object created.");
        Console.WriteLine($"IsInitialized: {obj.IsInitialized}");
    }
}