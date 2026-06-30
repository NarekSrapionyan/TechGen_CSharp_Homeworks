namespace Task3;

public class Initializer
{
    public static T CreateAndInitialize<T>()
        where T : IInitializable, new()
    {
        T obj = new T();

        obj.Initialize();

        return obj;
    }
}