using System.Linq;
using Nono.Engine.B.Logging;

namespace Nono.Engine.B
{
    public class Solution
    {
        public Solution(Field field, Performance time)
        {
            Field = field;
            Time = time;
            IsSolved = !field.Any(x => x == Box.Empty);
        }

        public Field Field { get; }

        public Performance Time { get; }

        public bool IsSolved { get; }
    }
}