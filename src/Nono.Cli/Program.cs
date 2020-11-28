using System;
using System.IO;

using CommandLine;

using Nono.Engine;
using Nono.Engine.IO;
using Nono.Engine.Logging;

namespace Nono.Cli
{
    class Program
    {
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Show action log")]
            public bool Verbose { get; set; }

            [Value(0, MetaName = "path", Required = true, HelpText = "Path to .non format file")]
            public string Path { get; set; }
        }

        public static class ExitCodes
        {
            public const int SUCCESS = 0;
            public const int NOT_SOLVED = 1;
            public const int FILE_NOT_FOUND = 2;
        }

        public class FileNotSolvedException : Exception
        {
            public FileNotSolvedException(string filepath)
                : base($"NOT SOLVED: {filepath}")
            {
                FilePath = filepath;
            }

            public string FilePath { get; }
        }

        public static int Main(string[] args)
        {
            try
            {
                Parser.Default
                    .ParseArguments<Options>(args)
                    .WithParsed<Options>(Run);

                return ExitCodes.SUCCESS;
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ExitCodes.FILE_NOT_FOUND;
            }
            catch (FileNotSolvedException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ExitCodes.NOT_SOLVED;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return ExitCodes.NOT_SOLVED;
            }
        }

        private static void Run(Options options)
        {
            using (var reader = new NonFileReader(Path.GetFileName(options.Path), File.OpenRead(options.Path)))
            {
                var nonogram = reader.Read();

                var solver = new Solver(GetLogger(options));
                var solution = solver.Solve(nonogram);

                Console.WriteLine(solution.Field);
                if (!solution.IsSolved)
                    throw new FileNotSolvedException(options.Path);
                Console.WriteLine($"Run time: {solution.Time}");
            }

        }

        private static ILog GetLogger(Options options)
        {
            if (options.Verbose)
                return new ConsoleLog();

            return new NullLog();
        }
    }
}
