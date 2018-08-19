namespace Nono.Engine.Tests.Suits
{
    public class TestCase
    {
        public TestCase(string title, uint[][] rows, uint[][] columns, Field goal)
        {
            Title = title;
            Rows = rows;
            Columns = columns;
            Goal = goal;
        }

        public string Title { get; }

        public uint[][] Rows { get; }

        public uint[][] Columns { get; }

        public Field Goal { get; }

        public override string ToString() => Title;
    }
}
