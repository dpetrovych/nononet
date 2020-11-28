using Nono.Engine.Helpers;
using Nono.Engine.Logging;
using Nono.Engine.Tests.Suits;
using Xunit;
using Xunit.Abstractions;

namespace Nono.Engine.Tests
{
    public class FullTests
    {
        private readonly ITestOutputHelper output;
        private readonly Solver solver = new Solver(new NullLog());

        public FullTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [TestFiles]
        public void TestOutput(Nonogram nonogram)
        {
            AssertAsync.CompletesIn(5, () =>
            {
                var result = solver.Solve(nonogram);

                output.WriteLine(result.Field.ToString());
                output.WriteLine($"Time: {result.Time}");
            });
        }
    }
}
