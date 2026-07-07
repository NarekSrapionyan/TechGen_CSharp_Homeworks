using Task1.Core_Engine;
namespace Task1.Specifications.Product;

public class MinPriceSpecification : ISpecification<Domain_Models.Product>
{
    private readonly decimal _minPrice;

    public MinPriceSpecification(decimal minPrice)
    {
        _minPrice = minPrice;
    }

    public bool IsSatisfiedBy(Domain_Models.Product candidate)
    {
        return candidate.Price >= _minPrice;
    }
}