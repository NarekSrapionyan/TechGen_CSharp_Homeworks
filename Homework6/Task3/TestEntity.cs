namespace Task3;

public class TestEntity : IInitializable
{
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        IsInitialized = true;
    }
}