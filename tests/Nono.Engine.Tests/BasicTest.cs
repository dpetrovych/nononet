using System.Linq;
using FluentAssertions;
using Nono.Engine.Log;
using Xunit;

namespace Nono.Engine.Tests
{
    public class BasicTest
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 100)]
        [InlineData(100, 1)]
        [InlineData(3, 3)]
        [InlineData(3, 2)]
        [InlineData(2, 3)]
        public void BulkFill_Rectangle(int rowPower, int columnPower)
        {
            // Arrange
            var rowHint = new[] { (uint)columnPower };
            var columnHint = new[] { (uint)rowPower };

            var solve = new Solver(new NullLog());
            var nonogram = new Nonogram(Enumerable.Repeat(rowHint, rowPower), Enumerable.Repeat(columnHint, columnPower));
            var result = solve.Solve(nonogram);

            result.Field.RowCount.Should().Be(rowPower);
            result.Field.ColumnCount.Should().Be(columnPower);
            result.Field.All(x => x == Box.Filled).Should().BeTrue();
        }
    }
}
