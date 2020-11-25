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
            ['1'] = Box.Filled,
        };

        public static ReadOnlySpan<Box> AsFieldLineSpan(this string str)
        {
            return new FieldLine(str.Select(chr => Map.TryGetValue(chr, out var box) ? box : Box.Empty)).AsSpan();
        }
    }
}