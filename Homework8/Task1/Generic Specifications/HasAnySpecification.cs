namespace Task1.Generic_Specifications;
using Task1.Core_Engine;


public class HasAnySpecification<TSource, TValue> : ISpecification<TSource>
{
    private readonly Func<TSource, IEnumerable<TValue>> _selector;
    private readonly ISpecification<TValue> _specification;

    public HasAnySpecification(Func<TSource, IEnumerable<TValue>> selector, ISpecification<TValue> specification)
    {
        _selector = selector;
        _specification = specification;
    }

    public bool IsSatisfiedBy(TSource candidate)
    {
        foreach (var value in _selector(candidate))
        {
            if (_specification.IsSatisfiedBy(value))
            {
                return true;
            }
        }

        return false;
    }
}