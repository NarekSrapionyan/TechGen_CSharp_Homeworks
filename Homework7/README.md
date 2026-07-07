# Generic Cache with Expiration

A lightweight generic in-memory cache implementation in **C#** that supports storing values by string keys with configurable expiration times.

## Features

* Generic cache (`Cache<T>`)
* Fast lookups using `Dictionary`
* Configurable expiration with `TimeSpan`
* Automatic expiration check on retrieval
* Manual cache invalidation
* Safe retrieval using `TryGet`

## Project Structure

```text
Task1
│
├── Cache.cs
├── CacheEntry.cs
└── Program.cs
```

## Classes

### Cache<T>

Main cache implementation.

**Responsibilities**

* Store values by key
* Retrieve cached values
* Check expiration
* Remove expired entries
* Manually invalidate cache entries

### CacheEntry<T>

Represents a single cached item.

**Stores**

* Cached value
* Expiration time
* Expiration status

## Public API

### Set

```csharp
void Set(string key, T value, TimeSpan duration)
```

Stores a value with the specified lifetime.

### TryGet

```csharp
bool TryGet(string key, out T value)
```

Returns:

* `true` if the value exists and has not expired.
* `false` if the key does not exist or the cached value has expired.

Expired entries are automatically removed during retrieval.

### Invalidate

```csharp
void Invalidate(string key)
```

Removes a cached entry manually.

## Example

```csharp
var cache = new Cache<string>();

cache.Set("message", "Hello!", TimeSpan.FromSeconds(5));

if (cache.TryGet("message", out var value))
{
    Console.WriteLine(value);
}

Thread.Sleep(6000);

if (!cache.TryGet("message", out _))
{
    Console.WriteLine("Cache miss");
}
```

## How It Works

1. A value is stored using a string key.
2. An expiration time is calculated from the provided duration.
3. `TryGet` checks whether the entry exists.
4. If the entry has expired, it is removed from the cache.
5. Otherwise, the cached value is returned.

## Technologies

* C#
* .NET
* Generics
* Dictionary
* TimeSpan
* DateTime (UTC)

## Notes

* The cache performs **lazy expiration**. Expired entries are removed when they are accessed via `TryGet`.
* `Invalidate` allows manual removal before expiration.
* Lookups are efficient thanks to the underlying `Dictionary`.

## Learning Goals

This project demonstrates:

* Generic class design
* Composition
* Encapsulation
* Dictionary-based storage
* Cache expiration logic
* `TryGet` pattern
* Working with `TimeSpan` and `DateTime`
