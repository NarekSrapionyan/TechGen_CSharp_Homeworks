namespace Task1;

public class CacheEntry<T>
{
    public T Value { get; }
    public DateTime ExpiresAt { get; }
    
    public bool IsExpired => DateTime.UtcNow > ExpiresAt;

    public CacheEntry(T value, TimeSpan duration)
    {
        Value = value;
        ExpiresAt = DateTime.UtcNow.Add(duration);
    }


}