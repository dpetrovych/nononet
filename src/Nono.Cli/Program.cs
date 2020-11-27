using System;
using System.IO;

using CommandLine;

using Nono.Cli.Log;
using Nono.Engine;
using Nono.Engine.IO;
using Nono.Engine.Log;

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
                Console.Error.WriteLine(ex);
                return ExitCodes.FILE_NOT_FOUND;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return ExitCodes.NOT_SOLVED;
            }
        }

        private static void Run(Options options)
        {
            using (var reader = new NonFileReader(File.OpenRead(options.Path)))
            {
                var nonogram = reader.Read();

                var solver = new Solver(GetLogger(options));
                var field = solver.Solve(nonogram);

            }

        }

        private static ILog GetLogger(Options options) 
        {
            if (options.Verbose) 
                return new ConsoleLog();

            return new NullLog();
        }

        // static void OutputBox(Box[,] image)
        // {
        //     for (int i = 0; i < image.GetLength(0); i++)
        //     {
        //         for (int j = 0; j < image.GetLength(1); j++)
        //         {
        //             Console.Write(image[i, j]);
        //         }
        //     }
        // }
    }
}
