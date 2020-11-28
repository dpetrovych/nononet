using System;
using System.IO;
using BenchmarkDotNet.Attributes;

using Nono.Engine;
using Nono.Engine.IO;
using Nono.Engine.Logging;

namespace Nono.Benchmark
{
    public abstract class SolverBenchmark
    {
        private readonly Nonogram nonogram;
        public SolverBenchmark(string file)
        {
            using (var reader = new NonFileReader(File.OpenRead($".\\Data\\{file}")))
                this.nonogram = reader.Read();
        }

        [Benchmark(Baseline = true)]
        public Solution Solver() => new Solver(new NullLog()).Solve(nonogram);
    }

    public class TigerBenchmark : SolverBenchmark
    {
        public TigerBenchmark() : base("tiger.non") { }
    }

    public class SunBenchmark : SolverBenchmark
    {
        public SunBenchmark() : base("sun.non") { }
    }

    public class SwingBenchmark : SolverBenchmark
    {
        public SwingBenchmark() : base("swing.non") { }
    }
}