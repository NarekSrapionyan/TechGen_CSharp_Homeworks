using ACA.PriceEngine;
using PriceEngineWrapper;

namespace PriceEngineConsole;

internal class Program
{
    static void Main(string[] args)
    {
        var input = new PriceInput
        {
            Lines = new List<BasketLine>
            {
                new BasketLine
                {
                    UnitPrice = 20.00m,
                    Quantity = 5
                },

                new BasketLine
                {
                    UnitPrice = 10.00m,
                    Quantity = 5
                }
            },

            LoyaltyTier = 1,
            CouponAmount = 15.00m,
            VatRate = 0.20m
        };

        var engine = new PriceEngine();

        decimal originalResult = engine.CalculatePayable(input);
        decimal correctedResult = CorrectedPriceCalculator.CalculateCorrectedPayable(engine, input);

        Console.WriteLine("Price calculation results");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Original result:  {originalResult:F2}");
        Console.WriteLine($"Corrected result: {correctedResult:F2}");
    }
}