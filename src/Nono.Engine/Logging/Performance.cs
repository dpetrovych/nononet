using System.Diagnostics;

namespace Nono.Engine.Logging
{
    public readonly struct Performance
    {
        public readonly double EllapsedSeconds;

        public Performance(long startTicks, long endTicks)
        {
            EllapsedSeconds = (endTicks - startTicks) / 1.0e7;
        }

        public override string ToString()
            => $"{EllapsedSeconds:0.000}s";

        public static Performance Measure(long start)
            => new Performance(start, Stopwatch.GetTimestamp());
    }
}