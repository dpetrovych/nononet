using System;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public sealed class FieldLine : Line
    {
        public FieldLine(IEnumerable<Box> boxes, LineIndex index) : base(boxes)
        {
            Index = index;
        }

        public LineIndex Index { get; }

        public DiffLine Diff(Line line)
        {
            if (line.Length != Length)
                throw new ArgumentException("Mismatch in length", nameof(line));

            var diffEnumerator = Enumerable.Zip(
                this, line, (thisBox, otherBox) => thisBox == Box.Empty ? otherBox : thisBox);

            return new DiffLine(diffEnumerator, Index);
        }
    }

    public static class FieldLineExtensions
    {
        public static IEnumerable<int> IndexFromCenter(int length)
        {
            var middleLeft = (length - 1) >> 1;
            var middleRight = length >> 1;
            var oddShift = length % 2;

            if (middleLeft == middleRight)
                yield return middleLeft;

            for (var i = oddShift; i < middleRight + oddShift; i++)
            {
                yield return middleLeft - i;
                yield return middleRight + i;
            }
        }

        public static int FindCenterBox(ReadOnlySpan<Box> fieldSpan, Box value)
        {
            foreach (var i in IndexFromCenter(fieldSpan.Length))
            {
                if (fieldSpan[i] == value)
                    return i;
            }

            return -1;
        }

        public static (int start, int end) FindCenterBlock(this ReadOnlySpan<Box> fieldSpan, Box value)
        {
            int start = FindCenterBox(fieldSpan, value);
            int end = start;
            if (start < 0)
                return (start, end);
            
            while (++end < fieldSpan.Length && fieldSpan[end] == value);
            while (--start >= 0 && fieldSpan[start] == value);
            ++start;

            return (start, end);
        }

        public static bool Any(this ReadOnlySpan<Box> fieldSpan, Box value)
        {
            foreach (var box in fieldSpan)
            {
                if (box == value) return true;
            }

            return false;
        }
    }
}