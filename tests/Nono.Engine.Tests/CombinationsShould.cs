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
        [InlineData(new[] { 10 }, 12, 3L)]
        [InlineData(new[] { 2 }, 4, 3L)]
        [InlineData(new[] { 1 }, 3, 3L)]
        public void OneBlockCount(int[] task, int length, long expectedCount)
        {
            var count = Combinations.Count(task, length);

            count.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData(new[] { 2, 2 }, 6, 3L)]
        [InlineData(new[] { 2, 1 }, 5, 3L)]
        [InlineData(new[] { 1, 1 }, 4, 3L)]
        public void TwoBlocksCount(int[] task, int length, long expectedCount)
        {
            var count = Combinations.Count(task, length);

            count.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData(new[] { 6, 2, 16, 1 }, 35, 330L)]
        public void CountComplex(int[] task, int length, long expectedCount)
        {
            var count = Combinations.Count(task, length);

            count.Should().Be(expectedCount);
        }
    }
}