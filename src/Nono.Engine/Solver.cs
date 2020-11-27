using System;
using System.Diagnostics;
using System.Linq;
using Nono.Engine.Log;

namespace Nono.Engine
{
    public class Solver
    {
        private readonly ILog _log;

        public Solver(ILog log)
        {
            _log = log;
        }

        public Solution Solve(Nonogram nonogram)
        {
            var start = Stopwatch.GetTimestamp();
            var field = new Field(nonogram.Rows.Length, nonogram.Columns.Length);

            var tasks = _log.InitTasks(() => TaskCollection.Create(nonogram));

            var hotheap = new Hotheap(tasks);

            while (hotheap.TryPop(out var line))
            {
                var fieldLine = field.GetLine(line.Index);
                var diffLine = _log.Collapse(
                    line, fieldLine, 
                    (line, fieldLine) => {
                        var collapsedLine = line.Collapse(fieldLine);
                        return fieldLine.Diff(collapsedLine);
                    });

                hotheap.PushDiff(diffLine);
                field.Set(diffLine);
            }

            var runTime = Performance.Measure(start);

            return new Solution(field, runTime);
        }
    }
}