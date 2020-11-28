using System;
using Nono.Engine;
using Nono.Engine.Logging;

using static Nono.Engine.Logging.Performance;
using static System.Console;
using static System.Diagnostics.Stopwatch;

namespace Nono.Cli
{
    public class ConsoleLog : ILog
    {
        public Field InitField(Func<Field> action) 
        {
            WriteLine($"init field");
            var field = action();

            CancelKeyPress += (s, e) => Write(field);
            return field;
        }

        public TaskCollection InitTasks(Func<TaskCollection> action)
        {
            WriteLine($"init tasks start");
            long start = GetTimestamp();

            var result = action();

            var measure = Performance.Measure(start);
            WriteLine($"          end   count={result.Count} elapsed= {measure}");

            return result;
        }

        public DiffLine Collapse(TaskLine task, FieldLine line, Func<TaskLine, FieldLine, DiffLine> action) 
        {
            WriteLine($"collapse  start {task.Index:-5} line={line} taks=({string.Join(',', task.Cues)}) count= {task.CombinationsCount}");
            long start = GetTimestamp();

            var result = action(task, line);

            var measure = Performance.Measure(start);
            WriteLine($"          end   {result.Index:-5} diff={result} elapsed= {measure}");

            return result;
        }
    }
}