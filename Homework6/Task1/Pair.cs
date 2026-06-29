namespace Task1;

public class Pair<T1, T2>
{
    public readonly  T1 First;
    public readonly T2 Second;

    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }

    public Pair<T2, T1> SwapSides()
    {
        return new Pair<T2, T1>(Second, First);
    }
    
    public override string ToString()
    {
        return $"({First}, {Second})";
    }
}