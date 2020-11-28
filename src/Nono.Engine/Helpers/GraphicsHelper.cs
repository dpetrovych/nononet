using System;
using System.Text;

namespace Nono.Engine.Helpers
{
    public static class GraphicsHelper
    {
        private static readonly Func<Box, string> FieldMap = Map("██", "░░", "  ");
        private static readonly Func<Box, string> LineMap = Map("1", "x", " ");

        public static string Map(Field field)
        {
            var sb = new StringBuilder((field.ColumnCount * 2 + 4) * (field.RowCount + 2));
            sb.Append('┌');
            sb.Append('─', 2 * field.ColumnCount);
            sb.Append('┐');
            sb.AppendLine();

            for (int i = 0; i < field.RowCount; i++)
            {
                sb.Append('|');
                var row = field.GetRow(i);
                foreach (var cell in row)
                    sb.Append(FieldMap(cell));

                sb.Append('|');
                sb.AppendLine();
            }

            sb.Append('└');
            sb.Append('─', 2 * field.ColumnCount);
            sb.Append('┘');

            return sb.ToString();
        }

        public static string Map(Line line)
        {
            var sb = new StringBuilder(line.Length + 2);
            sb.Append('|');
            foreach (var cell in line)
                sb.Append(LineMap(cell));
            
            sb.Append('|');
            return sb.ToString();
        }


        public static Func<Box, string> Map(string filled, string crossed, string empty)
        {
            return box =>
            {
                switch (box)
                {
                    case Box.Filled: return filled;
                    case Box.Crossed: return crossed; 
                }

                return empty;
            };
        }
    }
}