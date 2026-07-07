using Task1.Core_Engine;

namespace Task1.Specifications.Product;

public class CategorySpecification : ISpecification<Domain_Models.Product>
{
    private readonly string _category;

    public CategorySpecification(string category)
    {
        _category = category;
    }

    public bool IsSatisfiedBy(Domain_Models.Product candidate)
    {
        return candidate.Category.Equals(_category, StringComparison.OrdinalIgnoreCase);
    }
}