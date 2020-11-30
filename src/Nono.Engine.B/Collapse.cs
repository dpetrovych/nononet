using System.Collections.Generic;
using System.Linq;

using Nono.Engine.B.Extensions;

using static Nono.Engine.B.Constraints;

using SBox = System.ReadOnlySpan<Nono.Engine.B.Box>;
using SCues = System.ReadOnlySpan<int>;

namespace Nono.Engine.B
{
    public static class Collapse
    {
        public static CollapseLine Run(SCues cues, SBox fieldLine)
        {
            if (cues.Length == 0 || (cues.Length == 1 && cues[0] == 0))
                return CollapseLine.Crossed(fieldLine.Length);

            var (crStart, crEnd) = fieldLine.FindCenterBlock(Box.Crossed);
            if (crStart >= 0)
                return DivideByCrossed(cues, fieldLine, crStart, crEnd);

            var (flStart, flEnd) = fieldLine.FindCenterBlock(Box.Filled);
            if (flStart >= 0)
                return DivideByFilled(cues, fieldLine, flStart, flEnd);

            return Inplace(cues, fieldLine);
        }

        private static CollapseLine DivideByCrossed(SCues cues, SBox fieldLine, int start, int end)
        {
            var collector = new CollapseCollector();
            for (int i = 0; i <= cues.Length; i++)
            {
                var result = PrioritizedRun(
                    cues[..i], fieldLine[..start],
                    cues[i..], fieldLine[end..]);

                if (!result.HasValue)
                    continue;

                var line = JoinParts(
                    result.Left.Boxes,
                    Enumerable.Repeat(Box.Crossed, end - start),
                    result.Right.Boxes);

                collector.Add(line, result.CombinationsCount);
            }

            return collector.ToCollapseLine();
        }

        private static CollapseLine DivideByFilled(SCues cues, SBox fieldLine, int start, int end)
        {
            var collector = new CollapseCollector();
            for (int cueIndex = 0; cueIndex < cues.Length; cueIndex++)
            {
                var cue = cues[cueIndex];
                for (int pos = end - cue; pos <= start; pos++)
                {
                    var posEnd = pos + cue;
                    var leftBum = cueIndex > 0 ? MIN_BLOCK_SPACE : 0;
                    var rightBum = cueIndex < cues.Length - 1 ? MIN_BLOCK_SPACE : 0;

                    var leftEdge = pos - leftBum;
                    var rightEdge = posEnd + rightBum;

                    if (leftEdge < 0 || rightEdge > fieldLine.Length
                        || fieldLine[leftEdge..pos].Any(Box.Filled)
                        || fieldLine[posEnd..rightEdge].Any(Box.Filled))
                        continue;

                    var result = PrioritizedRun(
                        cues[..cueIndex], fieldLine[..leftEdge],
                        cues[(cueIndex + 1)..], fieldLine[rightEdge..]);

                    if (!result.HasValue)
                        continue;

                    var line = JoinParts(
                        result.Left.Boxes,
                        Enumerable.Repeat(Box.Crossed, leftBum),
                        Enumerable.Repeat(Box.Filled, cue),
                        Enumerable.Repeat(Box.Crossed, rightBum),
                        result.Right.Boxes);

                    collector.Add(line, result.CombinationsCount);
                }
            }

            return collector.ToCollapseLine();
        }

        private static IEnumerable<Box> JoinParts(params IEnumerable<Box>[] parts)
            => parts.SelectMany(x => x);

        private static CollapseLine Inplace(SCues cues, SBox fieldLine)
        {
            var moveSpace = Combinations.Moves(cues, fieldLine.Length);
            if (moveSpace == 0)
            {
                var line = new Box[MIN_BLOCK_SPACE + fieldLine.Length];
                var cursor = 0;
                foreach (var cue in cues)
                {
                    for (int i = 0; i < MIN_BLOCK_SPACE; i++)
                        line[cursor + i] = Box.Crossed;

                    cursor += MIN_BLOCK_SPACE;
                    for (int i = 0; i < cue; i++)
                        line[cursor + i] = Box.Filled;

                    cursor += cue;
                }

                return new CollapseLine(line.Skip(MIN_BLOCK_SPACE), 1);
            }

            var combinationsCount = Combinations.Count(cues, fieldLine.Length);
            if (moveSpace >= cues.Max())
                return CollapseLine.Empty(fieldLine.Length, combinationsCount);

            {
                var line = new Box[fieldLine.Length];
                var cursor = 0;
                foreach (var cue in cues)
                {
                    for (var i = 0; i < moveSpace; i++)
                    {
                        line[cursor + i] = Box.Empty;
                    }

                    cursor += moveSpace;

                    for (var i = 0; i < cue - moveSpace; i++)
                    {
                        line[cursor + i] = Box.Filled;
                    }

                    cursor += (cue - moveSpace);

                    for (var i = 0; i < MIN_BLOCK_SPACE; i++)
                    {
                        line[cursor + i] = Box.Empty;
                    }

                    cursor += MIN_BLOCK_SPACE;
                }

                for (; cursor < fieldLine.Length; cursor++)
                {
                    line[cursor] = Box.Empty;
                }

                return new CollapseLine(line, combinationsCount);
            }
        }

        private static CollapseLinePair PrioritizedRun(
            SCues leftCues, SBox leftLine,
            SCues rightCues, SBox rightLine)
        {
            if (!ShouldRun(leftCues, leftLine) || !ShouldRun(rightCues, rightLine))
                return new CollapseLinePair();

            CollapseLine left;
            CollapseLine right;
            if (leftCues.Length <= rightCues.Length)
            {
                left = Run(leftCues, leftLine);
                right = left.HasValue
                    ? Run(rightCues, rightLine)
                    : new CollapseLine();
            }
            else
            {
                right = Run(rightCues, rightLine);
                left = right.HasValue
                    ? Run(leftCues, leftLine)
                    : new CollapseLine();
            }

            return new CollapseLinePair(left, right);
        }

        private static bool ShouldRun(SCues cues, SBox line)
        {
            if (cues.Length > 0)
                return Combinations.Moves(cues, line.Length) >= 0;

            return !line.Any(Box.Filled);
        }
    }
}