namespace Task2;

public static class Filter
{
    public static T[] FilterArray<T>(T[] source, Predicate<T> predicate)
    {
        int count = 0;
        for (int i = 0; i < source.Length; i++)
        {
            if (predicate(source[i]))
            {
                count++;
            }
        }

        int index = 0;
        T[] result = new T[count];
        for (int i = 0; i < source.Length; i++)
        {
            if (predicate(source[i]))
            {
                result[index] = source[i];
                index++;
            }
        }
        
        return result;
    }
}
    


    
