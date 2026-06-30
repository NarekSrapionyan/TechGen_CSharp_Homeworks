namespace Task2;

public static class Project
{
    public static Tout[] ProjectValues<Tin, Tout>(Tin[] source, Func<Tin, Tout> func)
    {
        Tout[] result = new Tout[source.Length];
        for (int i = 0; i < source.Length; i++)
        {
            result[i] = func(source[i]);
        }
        return result;
    }
}