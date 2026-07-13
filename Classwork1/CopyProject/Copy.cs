namespace CopyProject;

public static class Copy
{
    public static void Start(Options options)
    {
        using FileStream source = new FileStream(options.FileSource, FileMode.Open, FileAccess.Read);

        using FileStream destination = new FileStream(options.FileDestination, FileMode.Create, FileAccess.Write);

        byte[] buffer = new byte[options.BufferSize];

        long fileSize = source.Length;
        long copied = 0;

        DateTime startTime = DateTime.Now;

        int readBytes;

        Console.WriteLine("Copy started...");
        Console.WriteLine($"Buffer size: {options.BufferSize} bytes");

        while ((readBytes = source.Read(buffer, 0, buffer.Length)) > 0)
        {
            destination.Write(buffer, 0, readBytes);

            copied += readBytes;

            TimeSpan passedTime = DateTime.Now - startTime;

            double percent = fileSize == 0 ? 100 : (double)copied / fileSize * 100;

            double copiedMb = copied / 1024.0 / 1024.0;
            double totalMb = fileSize / 1024.0 / 1024.0;

            double speed = passedTime.TotalSeconds > 0 ? copied / passedTime.TotalSeconds : 0;

            long leftBytes = fileSize - copied;

            double secondsLeft = speed > 0 ? leftBytes / speed : 0;

            TimeSpan remaining = TimeSpan.FromSeconds(secondsLeft);

            Console.Write(
                $"\rProgress: {percent:0.0}% | " +
                $"Copied: {copiedMb:0.0} MB / {totalMb:0.0} MB | " +
                $"Remaining Time: {remaining:hh\\:mm\\:ss}");
        }

        destination.Flush();

        Console.WriteLine();
        Console.WriteLine("Copy finished.");
        Console.WriteLine($"Destination: {Path.GetFullPath(options.FileDestination)}");
    }
}