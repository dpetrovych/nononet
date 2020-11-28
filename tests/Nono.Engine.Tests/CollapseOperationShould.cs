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
        public void Run_5_blocks_tight()
        {
            var line = CollapseOperation.Run(
                new uint[] { 1, 1, 1, 4, 2 },
                "             ".AsSpan());

            line.Should().Equal("1x1x1x1111x11".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(1L);
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
            var line = CollapseOperation.Run(new uint[] { 2, 1 }, "     ".AsSpan());

            line.Should().Equal(" —   ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(3);
        }

        [Fact]
        public void Run_2_blocks_with_crossed()
        {
            var line = CollapseOperation.Run(new uint[] { 2, 1 }, "  00 0".AsSpan());

            line.Should().Equal("110010".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(1);
        }

        [Fact]
        public void Run_3_blocks()
        {
            var line = CollapseOperation.Run(new uint[] { 1, 2, 3 }, "          ".AsSpan());

            line.Should().Equal("       —  ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(10);
        }

        [Fact]
        public void Run_4_blocks_with_filled()
        {
            var line = CollapseOperation.Run(
                new uint[] { 3, 30, 1, 5 },
                "  —                                      —   ".AsSpan());

            line.Should().Equal("  —    ———————————————————————————      ——   ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(34);
        }

        [Fact]
        public void Run_10_blocks_with_division()
        {
            var line = CollapseOperation.Run(
                new uint[] { 4, 6, 1, 3, 2, 2, 3, 4, 1 },
                "xxxxx————x——————x— x———xxx——x——xx———x             ".AsSpan());

            line.Should().Equal("xxxxx————x——————x—xx———xxx——x——xx———x             ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(36);
        }

        [Fact]
        public void Run_8_blocks_200M_combinations()
        {
            var line = CollapseOperation.Run(
                new uint[] { 1, 2, 2, 1, 12, 1, 2, 2 },
                "             x                    — —  ——                                  ".AsSpan());

            line.Should().Equal("             x                    — —  ——                                  ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(177581035L);
        }

        [Fact]
        public void Run_3_blocks_reduce_count()
        {
            var line = CollapseOperation.Run(
                new uint[] { 4, 1, 6 },
                "            —                 ".AsSpan());

            line.Should().Equal("            —                 ".AsBoxEnumerable());
            line.CombinationsCount.Should().Be(274L);
        }
    }
}

// .\src\Nono.Cli\bin\Debug\netcoreapp3.1\Nono.Cli.exe .\tests\Nono.Engine.Tests\Data\tiger.non

//  ls .\tests\Nono.Engine.Tests\Data -file | % { .\src\Nono.Cli\bin\Debug\netcoreapp3.1\Nono.Cli.exe  $_.fullname }

