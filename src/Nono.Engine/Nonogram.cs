using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public class Nonogram
    {
        public Nonogram(IEnumerable<uint[]> rows, IEnumerable<uint[]> columns, string? title = null)
        {
            Rows = rows.ToArray();
            Columns = columns.ToArray();
            Title = title;
        }

        public string? Title { get; }

        public uint[][] Rows { get; }

        public uint[][] Columns { get; }

        public override string ToString()
            => Title ?? base.ToString();
    }
}