using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public sealed class CollapseLine : Line
    {
        public CollapseLine(IEnumerable<Box> boxes, long combinationsCount) : base(boxes)
        {
            CombinationsCount = combinationsCount;
        }

        public long CombinationsCount { get; }

        public static CollapseLine Empty(int length)
        {
            return new CollapseLine(Enumerable.Repeat(Box.Empty, length), 0);
        }
    }
}