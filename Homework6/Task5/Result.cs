namespace Task5;

public class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public Exception? Error { get; }
    public int Attempts { get; }

    public Result(bool success, T? value, Exception? error, int attempts)
    {
        Success = success;
        Value = value;
        Error = error;
        Attempts = attempts;
    }
}