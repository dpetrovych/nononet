using System.Diagnostics;

namespace Nono.Engine.B.Logging
{
    public readonly struct Performance
    {
        public readonly double ElapsedMilliSeconds;

        public Performance(long startTicks, long endTicks)
        {
            ElapsedMilliSeconds = (endTicks - startTicks) / 1.0e4;
        }

        public override string ToString()
            => $"{ElapsedMilliSeconds:0.000}ms";

        public static Performance Measure(long start)
            => new Performance(start, Stopwatch.GetTimestamp());
    }
}