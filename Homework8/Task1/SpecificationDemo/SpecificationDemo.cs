using Task1.Domain_Models;
using Task1.Specifications.Product;
using Task1.Core_Engine;
namespace Task1.SpecificationDemo;

public static class SpecificationDemo
{
    public static void Run()
    {
        List<Product> products =
        [
            new()
            {
                Name = "Laptop",
                Price = 1200,
                Stock = 5,
                Category = "Electronics"
            },

            new()
            {
                Name = "Keyboard",
                Price = 80,
                Stock = 3,
                Category = "Electronics"
            },

            new()
            {
                Name = "Monitor",
                Price = 95,
                Stock = 8,
                Category = "Electronics"
            },

            new()
            {
                Name = "Mouse",
                Price = 25,
                Stock = 0,
                Category = "Electronics"
            },

            new()
            {
                Name = "Desk",
                Price = 300,
                Stock = 2,
                Category = "Furniture"
            }
        ];

        var inStock = new InStockSpecification();
        var outOfStock = new OutOfStockSpecification();
        var electronics = new CategorySpecification("Electronics");
        var affordable = new MaxPriceSpecification(100);
        var premium = new MinPriceSpecification(500);

        var promoEligible = inStock
            .And(electronics)
            .And(affordable);

        var restockCandidates = outOfStock
            .And(electronics);

        var premiumElectronics = electronics
            .And(premium);

        Console.WriteLine("-- E-commerce product filtering");
        Console.WriteLine();

        Console.WriteLine("Promo eligible:");

        foreach (var product in products.Where(promoEligible))
        {
            Console.WriteLine($"- {product.Name} (${product.Price}, stock={product.Stock})");
        }

        Console.WriteLine();

        Console.WriteLine("Restock candidates:");

        foreach (var product in products.Where(restockCandidates))
        {
            Console.WriteLine($"- {product.Name} (stock={product.Stock})");
        }

        Console.WriteLine();

        Console.WriteLine("Premium electronics (first match):");

        var firstPremium = products.FirstOrDefault(premiumElectronics);

        if (firstPremium != null)
        {
            Console.WriteLine($"- {firstPremium.Name} (${firstPremium.Price})");
        }

        Console.WriteLine();

        Console.WriteLine($"Affordable electronics count: {products.Count(electronics.And(affordable))}");
    }
}