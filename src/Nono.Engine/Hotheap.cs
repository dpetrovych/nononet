using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    internal class Hotheap
    {
        private HashSet<TaskLine> _heap;

        public Tasks _tasks;

        public Hotheap(Tasks tasks)
        {
            _tasks = tasks;
            _heap = tasks.Where(task => task.IsHotEmpty).ToHashSet();
        }

        public TaskLine? Pop()
        {
            if (_heap.Count == 0)
                return null;

            var minCount = _heap.OrderBy(x => x.Count).First();
            _heap.Remove(minCount);
            return minCount;
        }
    }
}