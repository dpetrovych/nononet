using Nono.Engine.Helpers;
using Nono.Engine.Tests.Suits;
using Xunit;
using Xunit.Abstractions;

namespace Nono.Engine.Tests
{
    public class FullTests
    {
        private readonly ITestOutputHelper output;

        public FullTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [TestFiles]
        public void TestOutput(uint[][] rows, uint[][] columns)
        {
            var solve = new Solver();

            AssertAsync.CompletesIn(5, () =>
            {
                var result = solve.Run(rows, columns);

                output.WriteLine(GraphicsHelper.Map(result));
            });            
        }
    }
}
