using System;

namespace Nono.Engine.Extensions
{
    public static class SpanExtensions
    {
        public static uint Sum(this Span<uint> span)
        {
            var sum = 0u;
            foreach (var element in span)
            {
                sum += element;
            }
            return sum;
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