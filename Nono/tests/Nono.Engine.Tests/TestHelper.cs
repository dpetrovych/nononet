using System;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine.Tests
{
    public static class TestHelper
    {
        public static IEnumerable<Box[]> LinePredictionFromString(string output)
            => output.Split('|').Select(line =>
                line.Select(c => (Box) Enum.Parse(typeof(Box), c.ToString(), true)).ToArray());
    }
}