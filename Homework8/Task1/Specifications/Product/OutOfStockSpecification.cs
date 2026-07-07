using Task1.Core_Engine;
namespace Task1.Specifications.Product;

public class OutOfStockSpecification : ISpecification<Domain_Models.Product>
{
    public bool IsSatisfiedBy(Domain_Models.Product candidate)
    {
        return candidate.Stock == 0;
    }
}