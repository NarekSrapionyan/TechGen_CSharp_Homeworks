namespace Task1.Core_Engine;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T candidate);
}
