using System;

namespace Nono.Engine.Extensions
{
    public static class SpanExtensions
    {
        public static long Sum(this Span<uint> span)
        {
            long sum = 0L;
            foreach (var element in span)
            {
                sum += element;
            }
            return sum;
        }

        public static uint Max(this Span<uint> span)
        {
            uint max = span[0];
            for (int i = 1; i < span.Length; i++)
            {
                var element = span[i];
                max = element > max ? element : max;
            }
            return max;
        }

        public static void Fill<T>(this Span<T> span, Func<int, T> func)
        {
            for (var i = 0; i < span.Length; i++)
            {
                span[i] = func(i);
            }
        }
    }
}