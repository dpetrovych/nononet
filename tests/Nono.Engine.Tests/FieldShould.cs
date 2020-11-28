using System.Linq;
using FluentAssertions;
using Xunit;

namespace Nono.Engine.Tests
{
    public class FieldShould
    {
        [Fact]
        public void SetRow()
        {
            var field = new Field(2, 3);
            var boxes = new Box[] { Box.X, Box.O, Box.X };
            var index = new LineIndex(Orientation.Row, 0);

            field.Set(new DiffLine(boxes, index));
            var line = field.GetLine(index);

            line.Should().Equal(boxes);
        }

        [Fact]
        public void SetColumn()
        {
            var field = new Field(2, 3);
            var boxes = new Box[] { Box.X, Box.O };
            var index = new LineIndex(Orientation.Column, 1);

            field.Set(new DiffLine(boxes, index));
            var line = field.GetLine(index);

            line.Should().Equal(boxes);
        }
    }
}