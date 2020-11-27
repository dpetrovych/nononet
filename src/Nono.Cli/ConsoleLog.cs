using System;
using System.Diagnostics;
using Nono.Engine;
using Nono.Engine.Logging;

using static Nono.Engine.Logging.Performance;

namespace Nono.Cli
{
    public class ConsoleLog : ILog
    {
        TaskCollection InitTasks(Func<TaskCollection> action)
        {
            Console.WriteLine($"init tasks start");
            long start = Stopwatch.GetTimestamp();

            var result = action();

            var measure = Performance.Measure(start);
            Console.WriteLine($"          end count={result.Count} elapsed = {measure}");

            return result;
        }

        public DiffLine Collapse(TaskLine task, FieldLine line, Func<TaskLine, FieldLine, DiffLine> action) 
        {
            Console.WriteLine($"collapse  start {task.Index:-5} line = {line} count = {task.CombinationsCount}");
            long start = Stopwatch.GetTimestamp();

            var result = action(task, line);

            var measure = Performance.Measure(start);
            Console.WriteLine($"          end {result.Index:-5} diff={result} elapsed = {measure}");

            return result;
        }
    }
}