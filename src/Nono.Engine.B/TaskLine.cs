using System;

namespace Nono.Engine.B
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

        public long CombinationsCount { get; private set; }

        public CollapseLine Collapse(FieldLine fieldLine)
        {
            var result = Engine.B.Collapse.Run(this.Cues, fieldLine);
            if (!result.HasValue)
                throw new Exception("Line is unsolvable");

            CombinationsCount = result.CombinationsCount;
            return result;
        }

        public override string ToString()
            => $"{Index}: ({string.Join(", ", Cues)})";
    }
}