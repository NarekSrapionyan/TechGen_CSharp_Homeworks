namespace FactorySystem.Components;
using FactorySystem.Domain;
using System.Threading;
public class QualityChecker
{
    private readonly OrderLine _orderLine;
    private readonly Storage _storage;
    private readonly Random _random;
    private readonly int _passPercentage;
    private readonly int _checkDelayMs;
    private bool _shouldStop;

    public QualityChecker(OrderLine orderLine, Storage storage, int passPercentage, int checkDelayMs)
    {
        _orderLine = orderLine;
        _storage = storage;
        _passPercentage = passPercentage;
        _checkDelayMs = checkDelayMs;
        _random = new Random();
        _shouldStop = false;
    }

    public void Stop()
    {
        _shouldStop = true;
    }

    public void Work()
    {
        while (!_shouldStop || !_orderLine.IsEmpty)
        {
            Item? itemToCheck = _orderLine.TryTake();

            if (itemToCheck != null)
            {
                Thread.Sleep(_checkDelayMs);

                bool isPassed = _random.Next(100) < _passPercentage;

                if (isPassed)
                {
                    Console.WriteLine($"Checker: Item {itemToCheck.Id} PASSED.");
                    _storage.AddItem(itemToCheck);
                }
                else
                {
                    Console.WriteLine($"Checker: Item {itemToCheck.Id} FAILED. Dropped.");
                }
            }
            else
            {
                Thread.Sleep(50);
            }
        }

        Console.WriteLine("QualityChecker: Finished processing remaining queue.");
    }
}
