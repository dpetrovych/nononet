using System;
using System.Threading;
using FluentAssertions;
using Nono.Engine.Helpers;
using Nono.Engine.Tests.Suits;
using Xunit;
using Xunit.Abstractions;

namespace Nono.Engine.Tests
{
    public class FullTests
    {
        private readonly ITestOutputHelper _output;

        public FullTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [TestFiles("Data.Simple")]  // 4
        public void Simple(TestCase testCase)
        {
            AssertAsync.CompletesIn(1, SolveAction(testCase));
        }

        [Theory]
        [TestFiles("Data.Mid")]     // 28
        public void Mid(TestCase testCase)
        {
            AssertAsync.CompletesIn(20, SolveAction(testCase));
        }

        [Theory]
        [TestFiles("Data.Dificult")]     // 3
        public void Difficult(TestCase testCase)
        {
            AssertAsync.CompletesIn(120, SolveAction(testCase));
        }

        [Theory]
        [TestFiles("Data.Huge")]     // 5
        public void Huge(TestCase testCase)
        {
            AssertAsync.CompletesIn(120, SolveAction(testCase));
        }

        private Action<CancellationToken> SolveAction(TestCase testCase)
        {
            return token =>
            {
                var solve = new Solver();
                var result = solve.Run(testCase.Rows, testCase.Columns, token);
                _output.WriteLine(GraphicsHelper.Map(result));

                result.Should().Equal(testCase.Goal);
            };
        }
    }
}
