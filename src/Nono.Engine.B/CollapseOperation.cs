using System.Collections.Generic;
using System.Linq;

using Nono.Engine.B.Extensions;

using static Nono.Engine.B.Constraints;

using SBox = System.ReadOnlySpan<Nono.Engine.B.Box>;
using SCues = System.ReadOnlySpan<uint>;

namespace Nono.Engine.B
{
    public static class CollapseOperation
    {
        public static CollapseLine? Run(SCues cues, SBox fieldLine)
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

        private static CollapseLine? DivideByCrossed(SCues cues, SBox fieldLine, int start, int end)
        {
            var result = new CollapseCollector();
            for (int i = 0; i <= cues.Length; i++)
            {
                var (left, right) = PrioritizedRun(
                    cues[..i], fieldLine[..start],
                    cues[i..], fieldLine[end..]);

                if (left == null || right == null)
                    continue;

                var line = JoinParts(left, Enumerable.Repeat(Box.Crossed, end - start), right);
                result.Add(line, left.CombinationsCount * right.CombinationsCount);
            }

            return result.ToCollapseLine();
        }

        private static CollapseLine? DivideByFilled(SCues cues, SBox fieldLine, int start, int end)
        {
            var result = new CollapseCollector();
            for (int cueIndex = 0; cueIndex < cues.Length; cueIndex++)
            {
                var cue = (int)cues[cueIndex];

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

                    var (left, right) = PrioritizedRun(
                        cues[..cueIndex], fieldLine[..leftEdge],
                        cues[(cueIndex + 1)..], fieldLine[rightEdge..]);

                    if (left == null || right == null)
                        continue;

                    var line = JoinParts(
                        left,
                        Enumerable.Repeat(Box.Crossed, leftBum),
                        Enumerable.Repeat(Box.Filled, cue),
                        Enumerable.Repeat(Box.Crossed, rightBum),
                        right);

                    result.Add(line, left.CombinationsCount * right.CombinationsCount);
                }
            }

            return result.ToCollapseLine();
        }

        private static IEnumerable<Box> JoinParts(params IEnumerable<Box>[] parts)
            => parts.SelectMany(x => x);

        private static CollapseLine? Inplace(SCues cues, SBox fieldLine)
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

                    cursor += (int)cue;
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

                    cursor += (int)(cue - moveSpace);

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

        private static (CollapseLine? left, CollapseLine? right) PrioritizedRun(
            SCues leftCues, SBox leftLine,
            SCues rightCues, SBox rightLine)
        {
            if (!ShouldRun(leftCues, leftLine) || !ShouldRun(rightCues, rightLine))
                return (null, null);

            CollapseLine? left;
            CollapseLine? right;
            if (leftCues.Length <= rightCues.Length)
            {
                left = Run(leftCues, leftLine);
                right = left != null
                    ? Run(rightCues, rightLine)
                    : null;
            }
            else
            {
                right = Run(rightCues, rightLine);
                left = right != null
                    ? Run(leftCues, leftLine)
                    : null;
            }

            return (left, right);
        }

        private static bool ShouldRun(SCues cues, SBox line)
        {
            if (cues.Length > 0)
                return Combinations.Moves(cues, line.Length) >= 0;

            return !line.Any(Box.Filled);
        }
    }
}