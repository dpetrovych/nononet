using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public class AdvSolver
    {
        public Field Solve(Nonogram nonogram)
        {
            var field = new Field(nonogram.Rows.Length, nonogram.Columns.Length);
            var tasks = Tasks.Create(nonogram);


            return field;
        }
    }
}