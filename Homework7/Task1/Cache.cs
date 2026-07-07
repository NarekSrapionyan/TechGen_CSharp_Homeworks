namespace Task1;

public class Cache<T>
{
    private readonly Dictionary<string, CacheEntry<T>> _storage = new Dictionary<string, CacheEntry<T>>();

    public void Set(string key, T value, TimeSpan duration)
    {
        _storage[key] = new CacheEntry<T>(value, duration);
    }

    public bool TryGet(string key, out T? value)
    {
        if (_storage.TryGetValue(key, out CacheEntry<T>? entry))
        {
            if (!entry.IsExpired)
            {
                value = entry.Value;
                return true;
            }

            _storage.Remove(key);
        }

        value = default;
        return false;
    }

    public void Invalidate(string key)
    {
        _storage.Remove(key);
    }
}