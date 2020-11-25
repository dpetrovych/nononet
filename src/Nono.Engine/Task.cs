using System;
using System.Collections.Generic;
using System.Linq;

using Nono.Engine.Extensions;

namespace Nono.Engine
{
    internal class Task
    {
        public Task(uint[] cues, int length, LineIndex index)
        {
            Cues = cues;
            Index = index;
            IsHotEmpty = Combinations.IsHot(cues, length);
            Count = Combinations.Count(cues, length);
        }

        public uint[] Cues { get; }

        public LineIndex Index { get; }

        public bool IsHotEmpty { get; }

        public Line Collapse(FieldLine fieldLine)
        {
            return Collapse(this.Cues, fieldLine.AsSpan());
        }

        private static Line Collapse(uint[] cues, ReadOnlySpan<Box> fieldLine)
        {
            return new CollapseLine(Enumerable.Empty<Box>(), 0);
        }

        public long Count { get; private set; }
    }
}