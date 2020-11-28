using FluentAssertions;
using Nono.Engine.Tests.Extensions;
using Xunit;

namespace Nono.Engine.Tests
{
    public class CollapseOperationShould
    {
        [Fact]
        public void Run_1_block()
        {
            var line = CollapseOperation.Run(new[] { 3u }, "     ".AsSpan());

            line.Should().Equal("  —  ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(3);
        }

        [Fact]
        public void Run_1_block_with_crossed()
        {
            var line = CollapseOperation.Run(new[] { 2u }, "    xxxxxxxx  xxxxx ".AsSpan());

            line.Should().Equal("    xxxxxxxx  xxxxxx".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(4);
        }

        [Fact]
        public void Run_2_blocks()
        {
            var line = CollapseOperation.Run(new[] { 2u, 1u }, "     ".AsSpan());

            line.Should().Equal(" —   ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(3);
        }

        [Fact]
        public void Run_2_blocks_with_crossed()
        {
            var line = CollapseOperation.Run(new[] { 2u, 1u }, "  00 0".AsSpan());

            line.Should().Equal("110010".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(1);
        }

        [Fact]
        public void Run_3_blocks()
        {
            var line = CollapseOperation.Run(new[] { 1u, 2u, 3u }, "          ".AsSpan());

            line.Should().Equal("       —  ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(10);
        }

        [Fact]
        public void Run_4_blocks_with_filled()
        {
            var line = CollapseOperation.Run(
                new[] { 3u, 30u, 1u, 5u },
                "  —                                      —   ".AsSpan());

            line.Should().Equal("  —    ———————————————————————————      ——   ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(34);
        }

        [Fact]
        public void Run_10_blocks_with_division()
        {
            var line = CollapseOperation.Run(
                new[] { 4u, 6u, 1u, 3u, 2u, 2u, 3u, 4u, 1u },
                "xxxxx————x——————x— x———xxx——x——xx———x             ".AsSpan());

            line.Should().Equal("xxxxx————x——————x—xx———xxx——x——xx———x             ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(36);
        }

        [Fact]
        public void Run_8_blocks_200M_combinations()
        {
            var line = CollapseOperation.Run(
                new[] { 1u, 2u, 2u, 1u, 12u, 1u, 2u, 2u },
                "             x                    — —  ——                                  ".AsSpan());

            line.Should().Equal("             x                    — —  ——                                  ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(177581035L);
        }

        [Fact]
        public void Run_6_blocks_count()
        {
            var line = CollapseOperation.Run(
                new[] { 6u, 2u, 16u, 1u },
                "                          —        ".AsSpan());

            line.Should().Equal("                 ——————————        ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(330L);
        }
    }
}