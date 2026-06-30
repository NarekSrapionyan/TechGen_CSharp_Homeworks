namespace Task4;

public class TopBuffer<T>
{
    private readonly T[] _buffer; 
    private readonly IComparer<T> _comparer;
    private int _count;

    public TopBuffer(int capacity, IComparer<T> comparer = null)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than zero.");
        }
        
        _buffer = new T[capacity];
        _comparer = comparer ?? Comparer<T>.Default;
        _count = 0;
    }

    public void Add(T item)
    {
        if (_count < _buffer.Length)
        {
            _buffer[_count] = item;
            _count++;
            SortDescending();

            return;
        }

        int lastIndex = _count - 1;
        if (_comparer.Compare(item, _buffer[lastIndex]) > 0)
        {
            _buffer[lastIndex] = item;
            SortDescending();
        }
    }
    private void SortDescending()
    {
        for (int i = 0; i < _count - 1; i++)
        {
            for (int j = 0; j < _count - i - 1; j++)
            {
                if (_comparer.Compare(_buffer[j], _buffer[j + 1]) < 0)
                {
                    T temp = _buffer[j];
                    _buffer[j] = _buffer[j + 1];
                    _buffer[j + 1] = temp;
                }
            }
        }
    }
    
    public T[] CopyArray()
    {
        T[] result = new T[_count];

        for (int i = 0; i < _count; i++)
        {
            result[i] = _buffer[i];
        }

        return result;
    }
}
