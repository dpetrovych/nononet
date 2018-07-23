using System;

namespace Nono.Engine.Tests.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this T[,] array, Action<int, int> action)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    action(i, j);
                }
            }
        }
    }
}