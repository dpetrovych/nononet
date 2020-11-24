using System;
using System.Collections.Generic;
using System.Linq;
using Nono.Engine.Extensions;
using Nono.Engine.Helpers;

namespace Nono.Engine
{
    public class LinePrediction
    {
        public static PredictionCollection Generate(uint[] definition, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (definition.Length == 0)
                return new PredictionCollection { Layout.Empty(length).Build() };

            var definitionSpace = DefinitionSpace(definition);

            if (definitionSpace > length)
                throw new ArgumentException($"Definition exeeds dimention limit, {definitionSpace} > {length}", nameof(definition));

            var layouts = GenerateInternal(definition, length);
            return new PredictionCollection( layouts.Select(l => l.Build()) );
        }

        private static IEnumerable<Layout> GenerateInternal(Span<uint> definition, int length)
        {
            var currentBlockLength = (int)definition[0];

            if (definition.Length == 1)
            {
                return Enumerable.Range(0, length - currentBlockLength + 1)
                    .Select(startSpan => Layout.Block(startSpan, currentBlockLength, length));
            }

            var subDefinition = definition.Slice(1);
            var subDefinitionSpace = DefinitionSpace(subDefinition);

            var predictions = new List<Layout>();
            for (var startSpan = 0; startSpan <= length - subDefinitionSpace - currentBlockLength - 1; startSpan++)
            {
                var startLine = Layout.Block(startSpan, currentBlockLength, startSpan + currentBlockLength);
                var subPredictions = GenerateInternal(subDefinition, length - startSpan - currentBlockLength - 1);

                predictions.AddRange(subPredictions.Select(subPrediction => startLine.Merge(subPrediction)));
            }

            return predictions;
        }

        private static int DefinitionSpace(Span<uint> definition) 
            => (int)(definition.Sum() + definition.Length - 1);


        private struct Layout
        {
            private int[] Value { get; set; }

            public static Layout Empty(int length)
            {
                return new Layout { Value = new[] { length } };
            }

            public static Layout Block(int startSpan, int blockLenght, int lineLength)
            {
                return new Layout { Value = new [] { startSpan, blockLenght, lineLength - startSpan - blockLenght } };
            }

            public Layout Merge(Layout other)
            {
                var merge = new int[Value.Length + other.Value.Length - 1];

                int lastIndex;
                var cutLength = lastIndex = Value.Length - 1;

                Value.AsSpan(0, cutLength).CopyTo(merge.AsSpan(0, cutLength));
                merge[lastIndex] = Value[lastIndex] + other.Value[0] + 1;

                other.Value.AsSpan(1).CopyTo(merge.AsSpan(cutLength + 1));

                return new Layout { Value = merge };
            }

            public Line Build()
            {
                var result = new Box[Value.Sum()];
                var current = 0;
                for (var spanIndex = 0; spanIndex < Value.Length; spanIndex++)
                {
                    var isCrossOut = spanIndex % 2 == 0;
                    var currentSpan = Value[spanIndex];

                    for (var i = 0; i < currentSpan; i++)
                    {
                        result[current++] = isCrossOut ? Box.CrossedOut : Box.Filled;
                    }
                }

                return new Line(result);
            }


            public override string ToString()
            {
                return GraphicsHelper.Map(Build());
            }
        }
    }
}