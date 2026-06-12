using System;
using System.Collections.Concurrent;
using FactorySystem.Domain;

namespace FactorySystem.Components
{
    public class Stock
    {
        private readonly ConcurrentQueue<Item>[] _shelves;

        public Stock()
        {
            _shelves = new ConcurrentQueue<Item>[3];
            _shelves[0] = new ConcurrentQueue<Item>();
            _shelves[1] = new ConcurrentQueue<Item>();
            _shelves[2] = new ConcurrentQueue<Item>();
        }

        public void AddItem(Item item)
        {
            int shelfIndex = (int)item.Type;
            _shelves[shelfIndex].Enqueue(item);
            Console.WriteLine($"Stock: Item {item.Id} of Type {item.Type} permanently stored.");
        }
    }
}