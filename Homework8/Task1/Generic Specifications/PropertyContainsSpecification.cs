using Task1.Core_Engine;
namespace Task1.Generic_Specifications;
public class PropertyContainsSpecification<TSource> : ISpecification<TSource>
{
    private readonly Func<TSource, string> _selector;
    private readonly string _text;

    public PropertyContainsSpecification(
        Func<TSource, string> selector,
        string text)
    {
        _selector = selector;
        _text = text;
    }

    public bool IsSatisfiedBy(TSource candidate)
    {
        string value = _selector(candidate);

        return value.Contains(_text, StringComparison.OrdinalIgnoreCase);
    }
}