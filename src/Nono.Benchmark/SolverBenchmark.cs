using System;
using System.IO;
using BenchmarkDotNet.Attributes;

using Nono.Engine;
using Nono.Engine.IO;
using Nono.Engine.Logging;

using SolverB = Nono.Engine.B.Solver;
using SolutionB = Nono.Engine.B.Solution;
using NullLogB = Nono.Engine.B.Logging.NullLog;
using NonogramB = Nono.Engine.B.Nonogram;
using NonFileReaderB = Nono.Engine.B.IO.NonFileReader;

namespace Nono.Benchmark
{
    public abstract class SolverBenchmark
    {
        private readonly Nonogram nonogram;
        private readonly NonogramB nonogramb;
        public SolverBenchmark(string file)
        {
            using (var reader = new NonFileReader(File.OpenRead($".\\Data\\{file}")))
                this.nonogram = reader.Read();

            using (var reader = new NonFileReaderB(File.OpenRead($".\\Data\\{file}")))
                this.nonogramb = reader.Read();
        }

        [Benchmark(Baseline = true)]
        public Solution Solver() => new Solver(new NullLog()).Solve(nonogram);

         [Benchmark]
        public SolutionB SolverB() => new SolverB(new NullLogB()).Solve(nonogramb);
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