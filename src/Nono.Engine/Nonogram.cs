using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public class Nonogram
    {
        public Nonogram(IEnumerable<uint[]> rows, IEnumerable<uint[]> columns)
        {
            Rows = rows.ToArray();
            Columns = columns.ToArray();
        }

        public uint[][] Rows { get; }

        public uint[][] Columns { get; }
    }
}