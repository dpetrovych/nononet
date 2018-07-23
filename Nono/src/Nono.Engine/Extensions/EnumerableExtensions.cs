using System;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Fill<T>(this T[,] array, Func<int, int, T> func)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = func(i, j);
                }
            }
        }

        public static void Fill<T>(this T[] array, Func<int, T> action)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = action(i);
            }
        }
    }
}