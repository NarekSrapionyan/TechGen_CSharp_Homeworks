namespace Task3;
using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string path = "download.bin";

        byte[] blockA = Encoding.UTF8.GetBytes("BLOCK_A");
        byte[] blockB = Encoding.UTF8.GetBytes("BLOCK_B");

        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
        {
            stream.Seek(0, SeekOrigin.Begin);
            stream.Write(blockA, 0, blockA.Length);

            stream.Seek(1024, SeekOrigin.Begin);
            stream.Write(blockB, 0, blockB.Length);

            stream.Flush();
        }

        using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            byte[] bufferA = new byte[blockA.Length];
            byte[] bufferB = new byte[blockB.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bufferA, 0, bufferA.Length);

            stream.Seek(1024, SeekOrigin.Begin);
            stream.Read(bufferB, 0, bufferB.Length);

            Console.WriteLine($"Block A: {Encoding.UTF8.GetString(bufferA)}");
            Console.WriteLine($"Block B: {Encoding.UTF8.GetString(bufferB)}");
        }
    }
}

