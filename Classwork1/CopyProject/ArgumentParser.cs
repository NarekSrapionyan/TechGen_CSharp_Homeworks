namespace CopyProject;

public static class ArgumentParser
{
    public static Options Parse(string[] args)
    {
        string? source = null;
        string? destination = null;
        int bufferSize = 4 * 1024 * 1024;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--source":

                    if (i + 1 >= args.Length)
                        throw new ArgumentException("Missing value for '--source'.");

                    source = args[++i];
                    break;

                case "--dest":

                    if (i + 1 >= args.Length)
                        throw new ArgumentException("Missing value for '--dest'.");

                    destination = args[++i];
                    break;

                case "--bsize":

                    if (i + 1 >= args.Length)
                        throw new ArgumentException("Missing value for '--bsize'.");

                    if (!int.TryParse(args[++i], out bufferSize))
                        throw new ArgumentException("Buffer size must be a valid integer.");

                    break;

                default:
                    throw new ArgumentException($"Unknown argument '{args[i]}'.");
            }
        }

        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException("Source path is required.");

        if (string.IsNullOrWhiteSpace(destination))
            throw new ArgumentException("Destination path is required.");

        return new Options(destination, source, bufferSize);
    }
}