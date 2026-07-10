namespace Task1;

class Program
{
    static void Main(string[] args)
    {
        string folderName = "reports";
        Directory.CreateDirectory(folderName);

        string path = Path.Combine(folderName, "report.txt");
        string reportContent = "asdadsadlkajad asdada\n asdasdad asdasd\n asdasdasd asdasdasda\n adasdadsda \n";

        File.WriteAllText(path, reportContent);
        string readContent = File.ReadAllText(path);

        if (reportContent == readContent)
        {
            Console.WriteLine("Saved!\n");
        }
        else
        {
            Console.WriteLine("Failed!\n");
        }
    }
}
