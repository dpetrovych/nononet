using System;
using System.IO;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

namespace Nono.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser.Default);
            BenchmarkRunner.Run(typeof(Program).Assembly, config);
        }
    }
}