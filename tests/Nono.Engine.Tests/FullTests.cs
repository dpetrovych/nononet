using Nono.Engine.Helpers;
using Nono.Engine.Tests.Suits;
using Xunit;
using Xunit.Abstractions;

namespace Nono.Engine.Tests
{
    public class FullTests
    {
        private readonly ITestOutputHelper output;
        private readonly Solver solver = new Solver();

        public FullTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [TestFiles]
        public void TestOutput(uint[][] rows, uint[][] columns)
        {
            var nonogram = new Nonogram(rows, columns);

            AssertAsync.CompletesIn(5, () =>
            {
                var result = solver.Solve(nonogram);

                output.WriteLine(GraphicsHelper.Map(result));
            });            
        }
    }
}
