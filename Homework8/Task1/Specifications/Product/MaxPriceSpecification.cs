using Task1.Core_Engine;
namespace Task1.Specifications.Product;

public class MaxPriceSpecification : ISpecification<Domain_Models.Product>
{
    private readonly decimal _maxPrice;

    public MaxPriceSpecification(decimal maxPrice)
    {
        _maxPrice = maxPrice;
    }

    public bool IsSatisfiedBy(Domain_Models.Product candidate)
    {
        return candidate.Price <= _maxPrice;
    }
}