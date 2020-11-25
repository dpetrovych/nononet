using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    internal class TaskCollection : IEnumerable<Task>
    {
        private readonly IReadOnlyDictionary<Orientation, Task[]> _tasks;

        private TaskCollection(IReadOnlyDictionary<Orientation, Task[]> tasks)
        {
            _tasks = tasks;
        }

        public Task this[Orientation orienation, int position] => _tasks[orienation][position];

        public static TaskCollection Create(Nonogram nonogram)
        {
            var tasks = new Dictionary<Orientation, Task[]>() {
                [Orientation.Column] = CreateTaskLines(nonogram.Columns, nonogram.Rows.Length, Orientation.Column),
                [Orientation.Row] = CreateTaskLines(nonogram.Rows, nonogram.Columns.Length, Orientation.Row),
            };

            return new TaskCollection(tasks);
        }

        private static Task[] CreateTaskLines(uint[][] tasksAxis, int length, Orientation orienation)
            => tasksAxis.Select((cues, i) => new Task(cues, length, new LineIndex(orienation, i))).ToArray();

        public IEnumerator<Task> GetEnumerator()
        {
            return _tasks.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}