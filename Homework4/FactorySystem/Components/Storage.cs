using System;
using FactorySystem.Domain;
using System.Collections.Concurrent;
namespace FactorySystem.Components;
public class Storage
{
    private readonly ConcurrentQueue<Item>[] _shelves;

    public Storage()
    {
        _shelves = new ConcurrentQueue<Item>[3];
        _shelves[0] = new ConcurrentQueue<Item>();
        _shelves[1] = new ConcurrentQueue<Item>();
        _shelves[2] = new ConcurrentQueue<Item>();
    }

    public bool IsEmpty => _shelves[0].IsEmpty && _shelves[1].IsEmpty && _shelves[2].IsEmpty;

    public void AddItem(Item item)
    {
        int shelfIndex = (int)item.Type;
        _shelves[shelfIndex].Enqueue(item);
    }

    public Item? TryTakeItem(ItemType type)
    {
        int shelfIndex = (int)type;
        if (_shelves[shelfIndex].TryDequeue(out Item item))
        {
            return item;
        }
        return null;
    }
}