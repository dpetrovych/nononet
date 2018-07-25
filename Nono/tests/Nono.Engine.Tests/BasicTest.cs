using System;
using System.Linq;
using FluentAssertions;
using Nono.Engine.Tests.Extensions;
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
            var rowHint = new[] { columnPower };
            var columnHint = new[] { rowPower };

            var solve = new Solver();
            var result = solve.Run(Enumerable.Repeat(rowHint, rowPower), Enumerable.Repeat(columnHint, columnPower));

            result.GetLength(0).Should().Be(rowPower);
            result.GetLength(1).Should().Be(columnPower);
            result.ForEach((i, j) => result[i, j].Should().Be(Box.Filled));
        }
    }
}
