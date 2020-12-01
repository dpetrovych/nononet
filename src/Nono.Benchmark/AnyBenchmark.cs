using System;

using BenchmarkDotNet.Attributes;

using Nono.Engine;

namespace Nono.Benchmark
{
    /// <summary>
    /// Finding out difference between for and foreach for ReadOnlySpan.
    /// From decompilation: generated code is identical.
    /// 
    ///|     Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
    ///|----------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
    ///| AnyForEach | 416.8 ns |  7.18 ns |  6.36 ns |  1.00 |    0.00 | 0.1259 |     - |     - |     528 B |
    ///|     AnyFor | 414.8 ns | 11.18 ns | 32.98 ns |  1.03 |    0.07 | 0.1259 |     - |     - |     528 B |
    /// </summary>

    public class AnyBenchmark
    {
        private readonly Box[] _testSeq = new Box[1000];

        [Benchmark(Baseline = true)]
        public bool AnyForEach() 
        {
            var span = _testSeq[250..750];
            foreach (var box in span)
            {
                if (box == Box.Filled) return true;
            }

            return false;
        }  

        [Benchmark]
        public bool AnyFor() 
        {
            var span = _testSeq[250..750];
            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] == Box.Filled) return true;
            }

            return false;
        }
    }
}