using System.Linq;
using Nono.Engine.Logging;

namespace Nono.Engine
{
    public class Solution
    {
        public Solution(Field field, Performance time)
        {
            Field = field;
            Time = time;
            IsSolved = field.Any(x => x == Box.Empty);
        }

        public Field Field { get; }

        public Performance Time { get; }

        public bool IsSolved { get; }
    }
}