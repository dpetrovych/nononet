using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine.B
{
    public sealed class DiffLine : Line
    {
        public DiffLine(IEnumerable<Box> boxes, LineIndex index) : base(boxes)
        {
            Index = index;
            NonEmptyIndexes = NonEmptyIndexesEnumerator(Boxes).ToArray();
        }

        public LineIndex Index { get; }

        public int[] NonEmptyIndexes { get; }

        private static IEnumerable<int> NonEmptyIndexesEnumerator(Box[] boxes)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i] != Box.Empty)
                    yield return i;
            }
        }
    }
}