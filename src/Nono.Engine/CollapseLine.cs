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

        public static CollapseLine Empty(int length, long combinationsCount)
            => new CollapseLine(Enumerable.Repeat(Box.Empty, length), combinationsCount);

        public static CollapseLine? Crossed(int length)
            => new CollapseLine(Enumerable.Repeat(Box.Crossed, length), 1);

        public static CollapseLine? Filled(int length)
            => new CollapseLine(Enumerable.Repeat(Box.Filled, length), 1);
    }
}