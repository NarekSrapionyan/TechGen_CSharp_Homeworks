namespace Task1.Generic_Specifications;
using Task1.Core_Engine;

public class HasAllSpecification<TSource, TValue> : ISpecification<TSource>
{
    private readonly Func<TSource, IEnumerable<TValue>> _selector;
    private readonly ISpecification<TValue> _specification;

    public HasAllSpecification(Func<TSource, IEnumerable<TValue>> selector, ISpecification<TValue> specification)
    {
        _selector = selector;
        _specification = specification;
    }

    public bool IsSatisfiedBy(TSource candidate)
    {
        bool hasValues = false;

        foreach (var value in _selector(candidate))
        {
            hasValues = true;

            if (!_specification.IsSatisfiedBy(value))
            {
                return false;
            }
        }

        return hasValues;
    }
}