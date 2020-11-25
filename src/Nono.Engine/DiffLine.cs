using System.Collections.Generic;

namespace Nono.Engine
{
    public sealed class DiffLine : Line
    {
        public DiffLine(IEnumerable<Box> boxes) : base(boxes)
        {
        }

        public IEnumerable<int> NonEmptyIndexes()
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i] != Box.Empty)
                    yield return i;
            }
        }
    }
}