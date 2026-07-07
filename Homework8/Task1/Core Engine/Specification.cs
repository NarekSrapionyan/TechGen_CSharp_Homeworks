namespace Task1.Core_Engine;

public static class Specification
{
    public static ISpecification<T> Create<T>(Func<T, bool> predicate)
    {
        return new PredicateSpecification<T>(predicate);
    }

    public static ISpecification<T> AllOf<T>(params ISpecification<T>[] specifications)
    {
        if (specifications.Length == 0)
            throw new ArgumentException("At least one specification is required.");

        ISpecification<T> result = specifications[0];

        for (int i = 1; i < specifications.Length; i++)
        {
            result = result.And(specifications[i]);
        }

        return result;
    }

    public static ISpecification<T> AnyOf<T>(params ISpecification<T>[] specifications)
    {
        if (specifications.Length == 0)
            throw new ArgumentException("At least one specification is required.");

        ISpecification<T> result = specifications[0];

        for (int i = 1; i < specifications.Length; i++)
        {
            result = result.Or(specifications[i]);
        }

        return result;
    }
}