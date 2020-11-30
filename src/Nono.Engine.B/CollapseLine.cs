using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine.B
{
    public readonly ref struct CollapseLinePair
    {
        public readonly CollapseLine Left;

        public readonly CollapseLine Right;
        

        public CollapseLinePair(CollapseLine left, CollapseLine right)
        {
            Left = left;
            Right = right;
        }

        public readonly bool HasValue => Left.HasValue && Right.HasValue;

        public readonly long CombinationsCount => Left.CombinationsCount * Right.CombinationsCount;
    }

    public readonly ref struct CollapseLine
    {
        public readonly Box[] Boxes;

        public readonly long CombinationsCount;

        public CollapseLine(IEnumerable<Box> boxes, long combinationsCount)
        {
            Boxes = boxes.ToArray();
            CombinationsCount = combinationsCount;
        }

        public readonly bool HasValue => Boxes != null;

        public static CollapseLine Empty(int length, long combinationsCount)
            => new CollapseLine(Enumerable.Repeat(Box.Empty, length), combinationsCount);

        public static CollapseLine Crossed(int length)
            => new CollapseLine(Enumerable.Repeat(Box.Crossed, length), 1);

        public static CollapseLine Filled(int length)
            => new CollapseLine(Enumerable.Repeat(Box.Filled, length), 1);
    }
}