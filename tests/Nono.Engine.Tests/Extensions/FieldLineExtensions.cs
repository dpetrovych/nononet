using System;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine.Tests.Extensions
{
    public static class FieldLineExtensions
    {
        public static readonly Dictionary<char, Box> Map = new Dictionary<char, Box>()
        {
            ['0'] = Box.Crossed,
            ['x'] = Box.Crossed,
            ['1'] = Box.Filled,
            ['—'] = Box.Filled,
        };

        public static ReadOnlySpan<Box> AsSpan(this string str)
        {
            return str.Select(chr => Map.TryGetValue(chr, out var box) ? box : Box.Empty).ToArray();
        }

        public static IEnumerable<Box> AsBoxEnumerable(this string str)
            => str.Select(chr => Map.TryGetValue(chr, out var box) ? box : Box.Empty);
    }
}