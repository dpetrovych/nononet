using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    internal class Tasks : IEnumerable<TaskLine>
    {
        private readonly IReadOnlyDictionary<TaskIndex, TaskLine> _tasks;

        private Tasks(IReadOnlyDictionary<TaskIndex, TaskLine> tasks)
        {
            _tasks = tasks;
        }

        public TaskLine this[Orienation orienation, int position] => _tasks[new TaskIndex(orienation, position)];
        public TaskLine this[TaskIndex taskIndex] => this[taskIndex];

        public static Tasks Create(Nonogram nonogram)
        {
            var rowLines = CreateTaskLines(nonogram.Rows, nonogram.Columns.Length, Orienation.Row);
            var columnLines = CreateTaskLines(nonogram.Columns, nonogram.Rows.Length, Orienation.Column);

            return new Tasks(Enumerable.Concat(rowLines, columnLines).ToDictionary(x => x.Index));
        }

        private static IEnumerable<TaskLine> CreateTaskLines(uint[][] tasksAxis, int length, Orienation orienation)
            => tasksAxis.Select((task, i) => new TaskLine(task, length, new TaskIndex(orienation, i)));

        public IEnumerator<TaskLine> GetEnumerator()
        {
            return _tasks.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}