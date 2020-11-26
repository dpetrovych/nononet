using System;

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

        public long Count { get; private set; }

        public Line Collapse(FieldLine fieldLine)
        {
            return CollapseOperation.Run(this.Cues, fieldLine)
                ?? throw new Exception("Line is unsolvable");
        }

        public override string ToString()
            => $"{Index}: ({string.Join(", ", Cues)})";
    }
}