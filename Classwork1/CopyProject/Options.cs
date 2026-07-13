namespace CopyProject;

public readonly struct Options
{
    public string FileDestination { get;}
    public string FileSource { get; }
    public int BufferSize { get;} 

    public Options(string fileDestination, string fileSource, int bufferSize)
    {
        FileDestination = fileDestination;
        FileSource = fileSource;
        BufferSize = bufferSize;
    }
}