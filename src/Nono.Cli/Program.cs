using System;
using Nono.Engine;

namespace Nono.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static void OutputBox(Box[,] image)
        {
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Console.Write(image[i, j]);
                }
            }
        }
    }
}
