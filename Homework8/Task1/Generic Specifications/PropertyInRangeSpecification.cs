using Task1.Core_Engine;

namespace Task1.Generic_Specifications;


public class PropertyInRangeSpecification<TSource, TValue> : ISpecification<TSource>
    where TValue : IComparable<TValue>
{
    private readonly Func<TSource, TValue> _selector;
    private readonly TValue _min;
    private readonly TValue _max;

    public PropertyInRangeSpecification(Func<TSource, TValue> selector, TValue min, TValue max)
    {
        _selector = selector;
        _min = min;
        _max = max;
    }

    public bool IsSatisfiedBy(TSource candidate)
    {
        TValue value = _selector(candidate);
        return value.CompareTo(_min) >= 0 && value.CompareTo(_max) <= 0;
    }
}