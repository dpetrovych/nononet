using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public class TaskCollection : IReadOnlyCollection<TaskLine>
    {
        private readonly IReadOnlyDictionary<Orientation, TaskLine[]> _tasks;

        private TaskCollection(IReadOnlyDictionary<Orientation, TaskLine[]> tasks, int count)
        {
            _tasks = tasks;
            Count = count;
        }

        public int Count { get; private set; }

        public TaskLine this[Orientation orienation, int position] => _tasks[orienation][position];

        public static TaskCollection Create(Nonogram nonogram)
        {
            var tasks = new Dictionary<Orientation, TaskLine[]>()
            {
                [Orientation.Column] = CreateTaskLines(nonogram.Columns, nonogram.Rows.Length, Orientation.Column),
                [Orientation.Row] = CreateTaskLines(nonogram.Rows, nonogram.Columns.Length, Orientation.Row),
            };

            return new TaskCollection(tasks, nonogram.Rows.Length + nonogram.Columns.Length);
        }

        private static TaskLine[] CreateTaskLines(uint[][] tasksAxis, int length, Orientation orienation)
            => tasksAxis.Select((cues, i) => new TaskLine(cues, length, new LineIndex(orienation, i))).ToArray();

        public IEnumerator<TaskLine> GetEnumerator()
            => _tasks.Values.SelectMany(x => x).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}