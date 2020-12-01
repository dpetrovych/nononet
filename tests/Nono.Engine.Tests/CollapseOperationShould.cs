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
            var line = Collapse.Run(new[] { 3 }, "     ".AsSpan());

            line.Boxes.Should().Equal("  —  ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(3);
        }

        [Fact]
        public void Run_5_blocks_tight()
        {
            var line = Collapse.Run(
                new[] { 1, 1, 1, 4, 2 },
                "             ".AsSpan());

            line.Boxes.Should().Equal("1x1x1x1111x11".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(1L);
        }

        [Fact]
        public void Run_1_block_with_crossed()
        {
            var line = Collapse.Run(new[] { 2 }, "    xxxxxxxx  xxxxx ".AsSpan());

            line.Boxes.Should().Equal("    xxxxxxxx  xxxxxx".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(4);
        }

        [Fact]
        public void Run_2_blocks()
        {
            var line = Collapse.Run(new[] { 2, 1 }, "     ".AsSpan());

            line.Boxes.Should().Equal(" —   ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(3);
        }

        [Fact]
        public void Run_2_blocks_with_crossed()
        {
            var line = Collapse.Run(new[] { 2, 1 }, "  00 0".AsSpan());

            line.Boxes.Should().Equal("110010".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(1);
        }

        [Fact]
        public void Run_3_blocks()
        {
            var line = Collapse.Run(new[] { 1, 2, 3 }, "          ".AsSpan());

            line.Boxes.Should().Equal("       —  ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(10);
        }

        [Fact]
        public void Run_4_blocks_with_filled()
        {
            var line = Collapse.Run(
                new[] { 3, 30, 1, 5 },
                "  —                                      —   ".AsSpan());

            line.Boxes.Should().Equal("  —    ———————————————————————————      ——   ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(34);
        }

        [Fact]
        public void Run_10_blocks_with_division()
        {
            var line = Collapse.Run(
                new[] { 4, 6, 1, 3, 2, 2, 3, 4, 1 },
                "xxxxx————x——————x— x———xxx——x——xx———x             ".AsSpan());

            line.Boxes.Should().Equal("xxxxx————x——————x—xx———xxx——x——xx———x             ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(36);
        }

        [Fact]
        public void Run_8_blocks_200M_combinations()
        {
            var line = Collapse.Run(
                new[] { 1, 2, 2, 1, 12, 1, 2, 2 },
                "             x                    — —  ——                                  ".AsSpan());

            line.Boxes.Should().Equal("             x                    — —  ——                                  ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(177581035L);
        }

        [Fact]
        public void Run_3_blocks_reduce_count()
        {
            var line = Collapse.Run(
                new[] { 4, 1, 6 },
                "            —                 ".AsSpan());

            line.Boxes.Should().Equal("            —                 ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(274L);
        }
    }
}
