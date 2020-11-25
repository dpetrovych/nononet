using System.Linq;
using FluentAssertions;
using Nono.Engine.Tests.Extensions;
using Xunit;

namespace Nono.Engine.Tests
{
    public class FieldLineShould
    {
        [Theory]
        [InlineData(0, new int[] { })]
        [InlineData(1, new int[] { 0 })]
        [InlineData(4, new int[] { 1, 2, 0, 3 })]
        [InlineData(5, new int[] { 2, 1, 3, 0, 4 })]
        public void GenerateIndexFromCenter(int length, int[] expectedIndexes)
        {
            var indexes = FieldLineExtensions.IndexFromCenter(length).ToArray();

            indexes.Should().BeEquivalentTo(expectedIndexes);
        }

        [Theory]
        [InlineData("  00   ", 2, 2)]
        [InlineData("0000   ", 0, 4)]
        [InlineData("   0000", 3, 4)]
        [InlineData("0000000", 0, 7)]
        [InlineData("111111", -1, 0)]
        public void FindCenterBlock(string line, int expectedStart, int expectedLength)
        {
            var (start, length) = line.AsFieldLineSpan().FindCenterBlock(Box.Crossed);

            start.Should().Be(expectedStart);
            length.Should().Be(expectedLength);
        }
    }
}