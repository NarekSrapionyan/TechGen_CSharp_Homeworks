using System;
using System.Threading;
using FactorySystem.Components;
using FactorySystem.Domain;

namespace FactorySystem.Components
{
    public class Simulation
    {
        public void Start()
        {
            Console.WriteLine("Initializing Factory Simulation...");

            int tickDurationMs = 1000; 
            int orderLineCapacity = 5;
            int transportCapacity = 6;
            int transportIntervalTicks = 4;

            OrderLine orderLine = new OrderLine(orderLineCapacity);
            Storage storage = new Storage();
            Stock stock = new Stock();

            QualityChecker checker = new QualityChecker(orderLine, storage, 80, 2 * tickDurationMs);
            
            Machines machineA = new Machines(ItemType.A, 100, orderLine, 1 * tickDurationMs, 35);
            Machines machineB = new Machines(ItemType.B, 200, orderLine, 2 * tickDurationMs, 18);
            Machines machineC = new Machines(ItemType.C, 300, orderLine, 3 * tickDurationMs, 12);

            TransportSystem transport = new TransportSystem(storage, stock, transportCapacity, transportIntervalTicks * tickDurationMs);

            Thread checkerThread = new Thread(checker.Work);
            Thread threadA = new Thread(machineA.Work);
            Thread threadB = new Thread(machineB.Work);
            Thread threadC = new Thread(machineC.Work);
            Thread transportThread = new Thread(transport.Work);

            checkerThread.Start();
            threadA.Start();
            threadB.Start();
            threadC.Start();
            transportThread.Start();

            while (!machineA.IsFinished || !machineB.IsFinished || !machineC.IsFinished)
            {
                Thread.Sleep(500);
            }

            checker.Stop();
            checkerThread.Join();

            transport.Stop();
            transportThread.Join();

            Console.WriteLine("Simulation finished. All plans completed and buffers cleared.");
        }
    }
}