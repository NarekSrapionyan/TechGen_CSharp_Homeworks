namespace Task2;

class Program
{
    static void Main(string[] args)
    {
        string inbox = "inbox";

        Directory.CreateDirectory(inbox);

        int count = 0;

        foreach (string file in Directory.EnumerateFiles(inbox))
        {
            FileInfo fileInfo = new FileInfo(file);

            Console.WriteLine($"File : {Path.GetFileName(file)}");
            Console.WriteLine($"Size : {fileInfo.Length} bytes");

            count++;
        }

        Console.WriteLine($"Count of Files: {count}");    
        
    }
}
