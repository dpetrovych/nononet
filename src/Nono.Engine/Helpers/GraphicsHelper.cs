using System;
using System.Linq;
using System.Text;

namespace Nono.Engine.Helpers
{
    public static class GraphicsHelper
    {
        public static string Map(Field field)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < field.RowCount; i++)
            {
                var row = field.GetRow(i);

                foreach (var cell in row)
                {
                    sb.Append(Map(cell));
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
                    sb.Append(Map(square[i, j]));
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static string Map(Line line)
        {
            return string.Join(null, line.Select(Map));
        }


        public static string Map(Box box)
        {
            switch (box)
            {
                case Box.Filled: return "██";
                case Box.Crossed: return "░░";
            }

            return "  ";
        }
    }
}