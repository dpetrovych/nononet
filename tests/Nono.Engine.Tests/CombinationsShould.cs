using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Nono.Engine.Tests
{
    public class CombinationsShould
    {
        private readonly ITestOutputHelper output;

        public CombinationsShould(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Theory]
        [InlineData(new uint[] {10}, 12, 3L)]
        [InlineData(new uint[] { 2},  4, 3L)]
        [InlineData(new uint[] { 1},  3, 3L)]
        public void OneBlockCount(uint[] task, int length, long expectedCount)
        {
            var count = Combinations.Count(task, length);

            count.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData(new uint[] {2, 2}, 6, 3L)]
        [InlineData(new uint[] {2, 1}, 5, 3L)]
        [InlineData(new uint[] {1, 1}, 4, 3L)]
        public void TwoBlocksCount(uint[] task, int length, long expectedCount)
        {
            var count = Combinations.Count(task, length);

            count.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData(new uint[] {6, 2, 16, 1}, 35, 330L)]
        public void CountComplex(uint[] task, int length, long expectedCount)
        {
            var count = Combinations.Count(task, length);

            count.Should().Be(expectedCount);
        }
    }
}