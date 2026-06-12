using FactorySystem.Domain;
using System;
using System.Threading;

namespace FactorySystem.Components;
public class Machines
{
    private readonly ItemType _type;
    private readonly OrderLine _orderLine;
    private int _currentId;
    private readonly int _productionDelayMs;
    private readonly int _totalItemsToProduce;
    private int _itemsProducedCount;

    public Machines(ItemType type, int startId, OrderLine orderLine, int productionDelayMs, int totalItemsToProduce)
    {
        _type = type;
        _currentId = startId;
        _orderLine = orderLine;
        _productionDelayMs = productionDelayMs;
        _totalItemsToProduce = totalItemsToProduce;
        _itemsProducedCount = 0;
    }

    public bool IsFinished => _itemsProducedCount >= _totalItemsToProduce;

    public void Work()
    {
        while (_itemsProducedCount < _totalItemsToProduce)
        {
            Thread.Sleep(_productionDelayMs);

            Item newItem = new Item(_currentId, _type);
            bool isAdded = _orderLine.TryAdd(newItem);

            if (isAdded)
            {
                Console.WriteLine($"Machine {_type}: Produced Item {newItem.Id} ({_itemsProducedCount + 1}/{_totalItemsToProduce})");
                _currentId++;
                _itemsProducedCount++;
            }
            else
            {
                Console.WriteLine($"Machine {_type}: OrderLine full! Item {newItem.Id} dropped.");
            }
        }

        Console.WriteLine($"Machine {_type}: Production completed.");
    }
}
