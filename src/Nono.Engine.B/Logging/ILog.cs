using System;

namespace Nono.Engine.B.Logging
{
    public interface ILog
    {
        Field InitField(Func<Field> action) 
            => action(); 

        TaskCollection InitTasks(Func<TaskCollection> action) 
            => action();

        DiffLine Collapse(TaskLine task, FieldLine line, Func<TaskLine, FieldLine, DiffLine> action) 
            => action(task, line);
    }

    public class NullLog : ILog { }
}