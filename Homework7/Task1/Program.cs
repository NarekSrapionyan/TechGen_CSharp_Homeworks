namespace Task1;

class Program
{
    static void Main(string[] args)
    {
        var cache = new Cache<string>();

        cache.Set("message", "Hello, World!", TimeSpan.FromSeconds(5));

        if (cache.TryGet("message", out var value))
        {
            Console.WriteLine(value);
        }

        Thread.Sleep(6000);

        if (cache.TryGet("message", out value))
        {
            Console.WriteLine(value);
        }
        else
        {
            Console.WriteLine("Cache expired");
        }

        cache.Set("message", "Hello again!", TimeSpan.FromSeconds(10));

        cache.Invalidate("message");

        if (!cache.TryGet("message", out value))
        {
            Console.WriteLine("Cache removed");
        }
        
    }
}