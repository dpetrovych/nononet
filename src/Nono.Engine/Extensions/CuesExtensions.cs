using System;

namespace Nono.Engine.Extensions
{
    public static class CuesExtensions
    {
        public static ushort Sum(this ReadOnlySpan<ushort> span)
        {
            // Sum of all cues should not be bigger then a max row count
            ushort sum = 0;
            for (int i = 0; i < span.Length; i++)
            {
                sum += span[i];
            }

            return sum;
        }

        public static ushort Max(this ReadOnlySpan<ushort> span)
        {
            ushort max = span[0];
            for (int i = 1; i < span.Length; i++)
            {
                var element = span[i];
                max = element > max ? element : max;
            }
            return max;
        }
    }
}