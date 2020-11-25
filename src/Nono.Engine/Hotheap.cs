using System;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    internal class Hotheap
    {
        private HashSet<Task> _heap;

        public TaskCollection _tasks;

        public Hotheap(TaskCollection tasks)
        {
            _tasks = tasks;
            _heap = tasks.Where(task => task.IsHotEmpty).ToHashSet();
        }

        public bool TryPop(out Task line)
        {
            line = _heap.OrderBy(x => x.Count).FirstOrDefault();
            if (line == null) 
                return false;

            _heap.Remove(line);
            return true;
        }

        public void PushDiff(DiffLine diff, Orientation orienation)
        {
            var opposite = orienation.Opposite();
            var oppsiteTasks = diff
                .NonEmptyIndexes()
                .Select(i => _tasks[opposite, i]);

            _heap.UnionWith(oppsiteTasks);
        }
    }
}