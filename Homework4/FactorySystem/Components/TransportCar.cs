using System;
using System.Threading;
using FactorySystem.Domain;

namespace FactorySystem.Components
{public class TransportSystem
    {
        private readonly Storage _storage;
        private readonly Stock _stock;
        private readonly int _capacity;
        private readonly int _intervalMs;
        private readonly Random _random;
        private bool _shouldStop;

        public TransportSystem(Storage storage, Stock stock, int capacity, int intervalMs)
        {
            _storage = storage;
            _stock = stock;
            _capacity = capacity;
            _intervalMs = intervalMs;
            _random = new Random();
            _shouldStop = false;
        }

        public void Stop()
        {
            _shouldStop = true;
        }

        public void Work()
        {
            while (!_shouldStop || !_storage.IsEmpty)
            {
                Thread.Sleep(_intervalMs);

                ItemType targetType = (ItemType)_random.Next(3);
                int itemsCollected = 0;

                while (itemsCollected < _capacity)
                {
                    Item? item = _storage.TryTakeItem(targetType);
                    if (item == null)
                    {
                        break;
                    }

                    _stock.AddItem(item);
                    itemsCollected++;
                }

                if (itemsCollected > 0)
                {
                    Console.WriteLine($"Transport: Moved {itemsCollected} items of Type {targetType} to Stock.");
                }
            }

            Console.WriteLine("TransportSystem: Finished delivering remaining stock.");
        }
    }
}