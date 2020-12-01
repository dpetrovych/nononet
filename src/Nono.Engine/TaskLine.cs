using System;

namespace Nono.Engine
{
    public class TaskLine
    {
        public TaskLine(int[] cues, int length, LineIndex index)
        {
            Cues = cues;
            Length = length;
            Index = index;
            CombinationsCount = Combinations.Count(cues, length);
        }

        public int[] Cues { get; }

        public int Length { get; }

        public LineIndex Index { get; }

        public decimal CombinationsCount { get; private set; }

        public CollapseLine Collapse(FieldLine fieldLine)
        {
            var result = Engine.Collapse.Run(this.Cues, fieldLine);
            if (!result.HasValue)
                throw new Exception("Line is unsolvable");

            CombinationsCount = result.CombinationsCount;
            return result;
        }

        public override string ToString()
            => $"{Index}: ({string.Join(", ", Cues)})";
    }
}