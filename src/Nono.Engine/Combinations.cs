using System;
using System.Collections.Generic;
using System.Linq;

using Nono.Engine.Extensions;

using static Nono.Engine.Constraints;

namespace Nono.Engine
{
    public static class Combinations
    {
        private static readonly Dictionary<(int, int), long> ZippedBlocksCountCache = new Dictionary<(int, int), long>();

        /// <summary>
        /// Calculate count of all combinations of n blocks of size 1 fit in length with spaces of size 1
        /// </summary>
        private static long CountZippedBlocks(int n, int length)
        {
            if (n == 1)
                return length;

            if (ZippedBlocksCountCache.TryGetValue((n, length), out long count))
                return count;

            int leftN = n >> 1;
            int rightN = n - leftN;
            int minL = leftN << 1 - 1;
            int minR = rightN << 1 - 1;

            int moves = length - minR - minL;

            var leftCounts = Enumerable.Range(minL, moves).Select(x => CountZippedBlocks(leftN, x));
            var rightCounts = Enumerable.Range(minR, moves).Select(x => CountZippedBlocks(rightN, x));

            long result = Enumerable.Zip(Derivative(leftCounts), rightCounts, (dleft, right) => dleft * right).Sum();

            return ZippedBlocksCountCache[(n, length)] = result;
        }

        private static IEnumerable<long> Derivative(IEnumerable<long> enumerable, long start = 0)
        {
            long prev = start;
            foreach (var item in enumerable)
            {
                yield return item - prev;
                prev = item;
            }
        }

        /// <summary>
        /// Calculates how many combinations of spans (thus block positions) available for a specific task for a line length
        /// Helps to prioritize reduce operations before calculating actual spans

        /// Uses divide &amp; conquer strategy by dividing line in 2 and calculating respective counts in left and right parts.
        /// Than assembles results by multiplying.

        /// Also reduces all blocks in task to length of 1 and space to length of 1.
        /// </summary>
        public static long Count(Span<uint> task, int length)
        {
            var extraBlockSpace = (MIN_BLOCK_SPACE - 1) * (task.Length - 1);
            var extraBlockLength = task.Sum() - task.Length;
            return CountZippedBlocks(task.Length, (int)(length - extraBlockSpace - extraBlockLength));
        }

        public static bool IsHot(Span<uint> task, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));

            var maxBlock = task.Length > 0 ? task.Max() : 0;
            return Moves(task, length) < maxBlock;
        }

        public static uint Moves(Span<uint> task, int length)
        {
            unchecked
            {
                return (uint)(length - task.Sum() - MIN_BLOCK_SPACE * (task.Length - 1));
            }
        }
    }
}