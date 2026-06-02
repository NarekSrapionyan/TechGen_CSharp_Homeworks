using System;

namespace Task2;

class Program
{
    static void Main(string[] args)
    {
        int[,] arr =
        {
            { 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 1, 0, 0, 1 },
            { 1, 0, 1, 0, 1, 0, 1, 1 },
            { 1, 0, 1, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1 }
        };


        Console.Write("Enter the matrix coordinates separated by comma (e.g., 0,0): ");
        string input = Console.ReadLine();
        
        var parts = input.Split(',');
        (int x, int y) point = (int.Parse(parts[0]), int.Parse(parts[1]));

        Console.Write("Enter the new fill value: ");
        int newValue = int.Parse(Console.ReadLine());

        Console.WriteLine("\nExecuting iterative flood fill...");
        FloodFiller.FillIterative(arr, point.x, point.y, newValue);

        Console.WriteLine("\nResulting Matrix:");
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                Console.Write($"{arr[i, j],3} ");
            }
            Console.WriteLine();
        }
    }
}

public class Queue
{
    private (int x, int y)[] arr = new (int x, int y)[10];
    private int front = 0;
    private int back = 0;

    public void Enqueue((int x, int y) value)
    {
        if (back == arr.Length)
        {
            Array.Resize(ref arr, arr.Length * 2);
        }
        arr[back++] = value;
    }

    public (int x, int y) Dequeue()
    {
        return arr[front++];
    }

    public bool IsEmpty()
    {
        return front == back;
    }
}

static class FloodFiller
{
    // 1. ITERATIVE METHOD
    public static void FillIterative(int[,] arr, int startX, int startY, int newValue)
    {
        if (arr == null) return;

        int n = arr.GetLength(0);
        int m = arr.GetLength(1);

        if (startX < 0 || startX >= n || startY < 0 || startY >= m) return;

        int currentValue = arr[startX, startY];
        if (currentValue == newValue) return;

        Queue queue = new Queue();
        
        queue.Enqueue((startX, startY));
        arr[startX, startY] = newValue; 

        // Direction vectors for moving up, down, left, and right
        int[] rowOffsets = { -1, 1, 0, 0 };
        int[] colOffsets = { 0, 0, -1, 1 };

        while (!queue.IsEmpty())
        {
            (int cx, int cy) = queue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nextX = cx + rowOffsets[i];
                int nextY = cy + colOffsets[i];

                if (nextX >= 0 && nextX < n && nextY >= 0 && nextY < m && arr[nextX, nextY] == currentValue)
                {
                    arr[nextX, nextY] = newValue; 
                    queue.Enqueue((nextX, nextY)); 
                }
            }
        }
    }

    // 2. RECURSIVE METHOD
    public static void FillRecursive(int[,] arr, int startX, int startY, int newValue)
    {
        if (arr == null) return;

        int n = arr.GetLength(0);
        int m = arr.GetLength(1);

        if (startX < 0 || startX >= n || startY < 0 || startY >= m) return;

        int currentValue = arr[startX, startY];
        if (currentValue == newValue) return;

        FillRecursiveHelper(arr, startX, startY, currentValue, newValue, n, m);
    }

    private static void FillRecursiveHelper(int[,] arr, int x, int y, int oldValue, int newValue, int n, int m)
    {
        if (x < 0 || x >= n || y < 0 || y >= m) return;
        if (arr[x, y] != oldValue) return;

        arr[x, y] = newValue;

        FillRecursiveHelper(arr, x - 1, y, oldValue, newValue, n, m); // Up
        FillRecursiveHelper(arr, x + 1, y, oldValue, newValue, n, m); // Down
        FillRecursiveHelper(arr, x, y - 1, oldValue, newValue, n, m); // Left
        FillRecursiveHelper(arr, x, y + 1, oldValue, newValue, n, m); // Right
    }
}