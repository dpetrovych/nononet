using System.Collections.Generic;
using System.Linq;
using Nono.Engine.Extensions;

namespace Nono.Engine
{
    public class Solve
    {
        public Box[,] Run(IEnumerable<int[]> rows, IEnumerable<int[]> columns)
        {
            var rowsDef = rows.ToList();
            var columnsDef = columns.ToList();

            var result = new Box[rowsDef.Count, columnsDef.Count];
            result.Fill((i, j) => Box.Filled);

            return result;
        }

    }
}
