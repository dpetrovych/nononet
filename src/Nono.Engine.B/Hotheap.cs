using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine.B
{
    internal class Hotheap
    {
        private HashSet<TaskLine> _heap;

        public TaskCollection _tasks;

        public Hotheap(TaskCollection tasks)
        {
            _tasks = tasks;
            _heap = tasks.Where(task => Combinations.IsHot(task.Cues, task.Length)).ToHashSet();
        }

        public bool TryPop(out TaskLine line)
        {
            line = _heap.OrderBy(x => x.CombinationsCount).FirstOrDefault();
            if (line == null) 
                return false;

            _heap.Remove(line);
            return true;
        }

        public void PushDiff(DiffLine diff)
        {
            var opposite = diff.Index.Orienation.Opposite();
            var oppsiteTasks = diff.NonEmptyIndexes.Select(i => _tasks[opposite, i]);

            _heap.UnionWith(oppsiteTasks);
        }
    }
}