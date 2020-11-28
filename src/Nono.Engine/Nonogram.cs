using System;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public class Nonogram
    {
        public Nonogram(IEnumerable<ushort[]> rows, IEnumerable<ushort[]> columns, string? title = null)
        {
            Rows = rows.ToArray();
            Columns = columns.ToArray();
    
            // Puzzle size is harlly limited 32767 (short.MaxValue) in either dimention due to 2GB array limitation in .NET Core
            // While puzzle of size 32000 x 10 is feasable, it is hardly a practical one
            RowsCount = Rows.Length <= short.MaxValue 
                ? Convert.ToUInt16(Rows.Length) 
                : throw new ArgumentOutOfRangeException(nameof(rows), $"Too many rows (>{short.MaxValue})");

            ColumnsCount = Columns.Length <= short.MaxValue 
                ? Convert.ToUInt16(Columns.Length) 
                : throw new ArgumentOutOfRangeException(nameof(rows), $"Too many colums (>{short.MaxValue})");

            Title = title;
        }

        public string? Title { get; }

        public ushort[][] Rows { get; }

        public ushort[][] Columns { get; }

        public ushort RowsCount { get; }

        public ushort ColumnsCount { get; }

        public override string ToString()
            => Title ?? base.ToString();
    }
}