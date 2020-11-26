using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public class Solver
    {
        public Field Solve(Nonogram nonogram)
        {
            var field = new Field(nonogram.Rows.Length, nonogram.Columns.Length);
            var tasks = TaskCollection.Create(nonogram);
            var hotheap = new Hotheap(tasks);

            while (hotheap.TryPop(out var line))
            {
                var fieldLine = field.GetLine(line.Index);
                var collapsedLine = line.Collapse(fieldLine);
                var diffLine = fieldLine.Diff(collapsedLine);

                hotheap.PushDiff(diffLine, line.Index.Orienation);
                field.Set(diffLine);
            }

            return field;
        }
    }
}