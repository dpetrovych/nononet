using System;

namespace Nono.Engine.B.Extensions
{
    public static class CuesExtensions
    {
        public static int Sum(this ReadOnlySpan<int> span)
        {
            int sum = 0;
            foreach (var element in span)
            {
                sum += element;
            }
            return sum;
        }

        public static int Max(this ReadOnlySpan<int> span)
        {
            int max = span[0];
            for (int i = 1; i < span.Length; i++)
            {
                var element = span[i];
                max = element > max ? element : max;
            }
            return max;
        }
    }
}