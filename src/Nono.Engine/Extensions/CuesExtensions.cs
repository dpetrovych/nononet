using System;

namespace Nono.Engine.Extensions
{
    public static class CuesExtensions
    {
        public static int Sum(this ReadOnlySpan<int> span)
        {
            int sum = 0;
            for (int i = 0; i < span.Length; i++)
            {
                sum += span[i];
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