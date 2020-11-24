using System;
using System.Linq;

using Nono.Engine.Extensions;

namespace Nono.Engine
{
    internal enum Orienation
    {
        Row = 0,
        Column = 1,
    }

    internal record TaskIndex
    {
        public Orienation Orienation { get; }

        public int Position { get; }

        public TaskIndex(Orienation orienation, int position)
            => (Orienation, Position) = (orienation, position);
    }

    internal class TaskLine
    {
        public TaskLine(uint[] task, int length, TaskIndex index)
        {
            Task = task;
            Index = index;
            IsHotEmpty = Combinations.IsHot(task, length);
            Count = Combinations.Count(task, length);
        }

        public uint[] Task { get; }

        public TaskIndex Index { get; }

        public bool IsHotEmpty { get; }

        public long Count { get; private set; }
    }
}