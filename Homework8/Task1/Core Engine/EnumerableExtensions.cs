namespace Task1.Core_Engine;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Where<T>(
        this IEnumerable<T> source,
        ISpecification<T> specification)
    {
        foreach (var item in source)
        {
            if (specification.IsSatisfiedBy(item))
            {
                yield return item;
            }
        }
    }

    public static bool Any<T>(
        this IEnumerable<T> source,
        ISpecification<T> specification)
    {
        foreach (var item in source)
        {
            if (specification.IsSatisfiedBy(item))
            {
                return true;
            }
        }

        return false;
    }

    public static bool All<T>(
        this IEnumerable<T> source,
        ISpecification<T> specification)
    {
        foreach (var item in source)
        {
            if (!specification.IsSatisfiedBy(item))
            {
                return false;
            }
        }

        return true;
    }

    public static T? FirstOrDefault<T>(
        this IEnumerable<T> source,
        ISpecification<T> specification)
    {
        foreach (var item in source)
        {
            if (specification.IsSatisfiedBy(item))
            {
                return item;
            }
        }

        return default;
    }

    public static int Count<T>(
        this IEnumerable<T> source,
        ISpecification<T> specification)
    {
        int count = 0;

        foreach (var item in source)
        {
            if (specification.IsSatisfiedBy(item))
            {
                count++;
            }
        }

        return count;
    }
}