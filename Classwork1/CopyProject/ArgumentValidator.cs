namespace CopyProject;

public static class ArgumentValidator
{
    public static void Validate(Options options)
    {
        if (string.IsNullOrWhiteSpace(options.FileSource))
            throw new ArgumentException("Source path is required.");

        if (string.IsNullOrWhiteSpace(options.FileDestination))
            throw new ArgumentException("Destination path is required.");

        if (!File.Exists(options.FileSource))
            throw new FileNotFoundException("Source file does not exist.", options.FileSource);

        if (options.BufferSize <= 0)
            throw new ArgumentException("Buffer size must be greater than zero.");

        string sourceFullPath = Path.GetFullPath(options.FileSource);
        string destinationFullPath = Path.GetFullPath(options.FileDestination);

        if (string.Equals(sourceFullPath, destinationFullPath, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Source and destination paths cannot be the same.");

        string? destinationDirectory = Path.GetDirectoryName(destinationFullPath);

        if (!string.IsNullOrWhiteSpace(destinationDirectory))
        {
            Directory.CreateDirectory(destinationDirectory);
        }
    }
}