// using System;
// using FluentAssertions;
// using Xunit;

// namespace Nono.Engine.Tests.Internal
// {
//     public class LinePreditionShold
//     {
//         [Theory]
//         [InlineData(new [] {100u}, 99)]
//         [InlineData(new [] {1u, 1u}, 2)]
//         [InlineData(new [] {5u, 5u, 5u}, 16)]
//         public void Generate_WrongDefinition_Fail(uint[] defintion, int length)
//         {
//             Assert.Throws<ArgumentException>(() => LinePrediction.Generate(defintion, length));
//         }

        
//         [InlineData(new[] { 1u }, 1, "_")]
//         [InlineData(new[] { 3u }, 3, "___")]
//         [InlineData(new[] { 1u }, 3, "_XX|X_X|XX_")]
//         [InlineData(new[] { 2u }, 3, "__X|X__")]
//         [Theory]
//         public void Generate_SingleDefiniton_AllResults(uint[] defintion, int length, string output)
//         {
//             var expectedResult = TestHelper.LinePredictionFromString(output);

//             var actualResult = LinePrediction.Generate(defintion, length);

//             actualResult.Should().BeEquivalentTo(expectedResult);
//         }

        
//         [InlineData(new[] { 1u, 1u }, 3, "_X_")]
//         [InlineData(new[] { 1u, 1u }, 4, "_X_X|_XX_|X_X_")]
//         [InlineData(new[] { 1u, 1u }, 5, "_X_XX|_XX_X|_XXX_|X_X_X|X_XX_|XX_X_")]
//         [InlineData(new[] { 2u, 1u }, 5, "__X_X|__XX_|X__X_")]
//         [Theory]
//         public void Generate_DobleDefiniton_AllResults(uint[] defintion, int length, string output)
//         {
//             var expectedResult = TestHelper.LinePredictionFromString(output);

//             var actualResult = LinePrediction.Generate(defintion, length);

//             actualResult.Should().BeEquivalentTo(expectedResult);
//         }
//     }
// }