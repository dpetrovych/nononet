using System;

namespace Nono.Engine.Logging
{
    public interface ILog
    {
        TaskCollection InitTasks(Func<TaskCollection> action) 
            => action();

        DiffLine Collapse(TaskLine task, FieldLine line, Func<TaskLine, FieldLine, DiffLine> action) 
            => action(task, line);
    }

    public class NullLog : ILog { }
}