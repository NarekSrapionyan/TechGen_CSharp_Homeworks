using System.Collections.Concurrent;
using FactorySystem.Domain;

namespace FactorySystem.Components;
public class OrderLine
{
    private readonly ConcurrentQueue<Item> _queue;
    private readonly int _capacity;

    public OrderLine(int capacity)
    {
        _queue = new ConcurrentQueue<Item>();
        _capacity = capacity;
    }

    public bool IsEmpty => _queue.IsEmpty;

    public bool TryAdd(Item item)
    {
        if (_queue.Count >= _capacity)
        {
            return false;
        }
        _queue.Enqueue(item);
        return true;
    }

    public Item? TryTake()
    {
        if (_queue.TryDequeue(out Item item))
        {
            return item;
        }
        return null;
    }
}