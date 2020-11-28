using System.Collections.Generic;

namespace Nono.Engine
{
    public sealed class DiffLine : Line
    {
        public DiffLine(IEnumerable<Box> boxes, LineIndex index) : base(boxes)
        {
            Index = index;
        }

        public LineIndex Index { get; }

        public IEnumerable<ushort> NonEmptyIndexes()
        {
            for (ushort i = 0; i < Length; i++)
            {
                if (this[i] != Box.Empty)
                    yield return i;
            }
        }
    }
}