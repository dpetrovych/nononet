using System;
using System.Linq;
using System.Text;

namespace Nono.Engine.Helpers
{
    public static class GraphicsHelper
    {
        private static readonly Func<Box, string> FieldMap = Map("██", "░░", "  ");
        private static readonly Func<Box, string> LineMap = Map("—", "x", " ");

        public static string Map(Field field)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < field.RowCount; i++)
            {
                var row = field.GetRow(i);

                foreach (var cell in row)
                {
                    sb.Append(FieldMap(cell));
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static string Map(Box[,] square)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < square.GetLength(0); i++)
            {
                for (int j = 0; j < square.GetLength(1); j++)
                {
                    sb.Append(FieldMap(square[i, j]));
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static string Map(Line line)
        {
            return string.Join(null, line.Select(LineMap));
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