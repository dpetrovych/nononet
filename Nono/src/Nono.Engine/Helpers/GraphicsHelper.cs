using System;
using System.Linq;
using System.Text;

namespace Nono.Engine.Helpers
{
    public static class GraphicsHelper
    {
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
            return new string(line.Select(Map).ToArray());
        }

        public static char Map(Box box)
        {
            switch (box)
            {
                case Box.Filled: return '_';
                case Box.CrossedOut: return 'X';
            }

            return ' ';
        }
    }
}