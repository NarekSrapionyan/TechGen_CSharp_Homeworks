namespace Task1.Core_Engine;
public class NotSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _specification;

    public NotSpecification(ISpecification<T> specification)
    {
        _specification = specification;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return !_specification.IsSatisfiedBy(candidate);
    }
}