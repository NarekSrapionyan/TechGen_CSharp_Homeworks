// Task 3:
// This program implements a custom integer list using an internal int[] array.
// The list supports Add, AddRange, Remove, TryGet,
// dynamic resizing, and additional helper methods.

namespace Task3;

class Program
{
    static void Main(string[] args)
    {
        MyList list = new MyList();

        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Add(40);
        list.Add(50);

        list.AddRange(new int[] { 60, 70, 80 });

        Console.WriteLine("Initial list:");
        PrintList(list);

        Console.WriteLine();

        bool removed = list.Remove(30);
        Console.WriteLine($"Remove 30: {removed}");

        Console.WriteLine("After remove:");
        PrintList(list);

        Console.WriteLine();

        if (list.TryGet(2, out int value))
        {
            Console.WriteLine($"Item at index 2: {value}");
        }
        else
        {
            Console.WriteLine("Invalid index.");
        }

        Console.WriteLine($"Index of 70: {list.IndexOf(70)}");
        Console.WriteLine($"Contains 100: {list.Contains(100)}");

        list[0] = 999;
        Console.WriteLine("After changing item at index 0:");
        PrintList(list);

        Console.WriteLine();

        list.Clear();
        Console.WriteLine("After clear:");
        PrintList(list);
    }

    static void PrintList(MyList list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list.TryGet(i, out int item))
            {
                Console.Write(item + " ");
            }
        }

        Console.WriteLine();
    }

    public class MyList
    {
        private int[] _items;

        public int Count { get; private set; }

        public MyList()
        {
            _items = new int[4];
            Count = 0;
        }

        public void Add(int item)
        {
            if (Count == _items.Length)
            {
                Grow();
            }

            _items[Count] = item;
            Count++;
        }

        public void AddRange(int[] items)
        {
            if (items == null)
            {
                return;
            }

            for (int i = 0; i < items.Length; i++)
            {
                Add(items[i]);
            }
        }

        public bool Remove(int item)
        {
            int index = IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            for (int i = index; i < Count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            Count--;
            _items[Count] = 0;

            return true;
        }

        public bool TryGet(int index, out int value)
        {
            if (index < 0 || index >= Count)
            {
                value = 0;
                return false;
            }

            value = _items[index];
            return true;
        }

        public int IndexOf(int item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i] == item)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(int item)
        {
            return IndexOf(item) != -1;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                _items[i] = 0;
            }

            Count = 0;
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }

                return _items[index];
            }

            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }

                _items[index] = value;
            }
        }

        private void Grow()
        {
            int[] newItems = new int[_items.Length * 2];

            for (int i = 0; i < Count; i++)
            {
                newItems[i] = _items[i];
            }

            _items = newItems;
        }
    }
}