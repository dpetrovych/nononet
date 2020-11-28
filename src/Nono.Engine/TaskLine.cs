using System;

namespace Nono.Engine
{
    public class TaskLine
    {
        public TaskLine(uint[] cues, int length, LineIndex index)
        {
            Cues = cues;
            Index = index;
            IsHotEmpty = Combinations.IsHot(cues, length);
            CombinationsCount = Combinations.Count(cues, length);
        }

        public uint[] Cues { get; }

        public LineIndex Index { get; }

        public bool IsHotEmpty { get; }

        public long CombinationsCount { get; private set; }

        public Line Collapse(FieldLine fieldLine)
        {
            var result = CollapseOperation.Run(this.Cues, fieldLine)
                ?? throw new Exception("Line is unsolvable");
            CombinationsCount = result.CombinationsCount;
            return result;
        }

        public override string ToString()
            => $"{Index}: ({string.Join(", ", Cues)})";
    }
}