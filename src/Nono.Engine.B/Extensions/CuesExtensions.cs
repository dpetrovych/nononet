using System;

namespace Nono.Engine.B.Extensions
{
    public static class CuesExtensions
    {
        public static long Sum(this ReadOnlySpan<uint> span)
        {
            long sum = 0L;
            foreach (var element in span)
            {
                sum += element;
            }
            return sum;
        }

        public static uint Max(this ReadOnlySpan<uint> span)
        {
            uint max = span[0];
            for (int i = 1; i < span.Length; i++)
            {
                var element = span[i];
                max = element > max ? element : max;
            }
            return max;
        }
    }
}