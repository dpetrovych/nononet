using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Nono.Engine.Models;
using Nono.Engine.Prediction;

namespace Nono.Engine
{
    public class Solver
    {
        public Solution Run(IEnumerable<uint[]> rows, IEnumerable<uint[]> columns, CancellationToken token = default(CancellationToken))
        {
            var rowsDef = rows.ToList();
            var columnsDef = columns.ToList();

            Field field;
            var solution = new Solution();

            var rowsPredictions = rowsDef.Select(row => LinePrediction.Generate(row, columnsDef.Count)).ToArray();
            var columnsPredictions =
                columnsDef.Select(column => LinePrediction.Generate(column, rowsDef.Count)).ToArray();
            
            while (!Predict(rowsPredictions, columnsPredictions, out field))
            {
                solution.AddStep(field);

                var filterResult = Filter(rowsPredictions, columnsPredictions, field);

                if (filterResult == FilterResult.Unsolvable)
                    throw new InvalidOperationException("Field is unsolvable");

                token.ThrowIfCancellationRequested();
            }

            solution.AddResult(field);

            return solution;
        }

        


        private static bool Predict(
            PredictionCollection[] rowsPredictions, 
            PredictionCollection[] columnsPredictions,
            out Field fieldOut)
        {
            var field = new Field(rowsPredictions.Length, columnsPredictions.Length);

            void FillDefined(IEnumerable<Cell> fieldLine, Line calculatedLine)
            {
                foreach (var cell in fieldLine)
                {
                    if (cell == Box.Empty)
                        cell.Set(calculatedLine[cell.Index]);
                }
            }

            foreach (var rowIndex in Enumerable.Range(0, rowsPredictions.Length))
            {
                var currentPrediction = PredictLine(rowsPredictions[rowIndex]);
                FillDefined(field.GetRowEnumerator(rowIndex), currentPrediction);
            }

            foreach (var columnIndex in Enumerable.Range(0, columnsPredictions.Length))
            {
                var currentPrediction = PredictLine(columnsPredictions[columnIndex]);
                FillDefined(field.GetColumnEnumerator(columnIndex), currentPrediction);
            }


            fieldOut = field;
            return field.All(x => x != Box.Empty);
        }

        private static Line PredictLine(PredictionCollection currentPredictions)
        {
            if (currentPredictions.Count == 1)
                return currentPredictions[0];

            var currentPrediction = currentPredictions[0].ToBoxArray();

            foreach (var nextPrediction in currentPredictions.Skip(1))
            {
                for (int i = 0; i < currentPrediction.Length; i++)
                {
                    if (currentPrediction[i] != Box.Empty && currentPrediction[i] != nextPrediction[i])
                        currentPrediction[i] = Box.Empty;
                }
            }

            return new Line(currentPrediction);
        }

        private FilterResult Filter(PredictionCollection[] rowsPredictions, PredictionCollection[] columnsPredictions, Field field)
        {
            var anyChanged = false;

            bool PredictionFits(Line prediction, Line fieldLine)
            {
                for (int i = 0; i < fieldLine.Count; i++)
                {
                    if (fieldLine[i] != Box.Empty && fieldLine[i] != prediction[i])
                        return false;
                }

                return true;
            }

            PredictionCollection FilterLine(PredictionCollection predictions, IEnumerable<Box> fieldLine)
            {
                var filteredPrediction =
                    new PredictionCollection(predictions.Where(prediction => PredictionFits(prediction, new Line(fieldLine.ToArray()))));

                anyChanged = anyChanged || filteredPrediction.Count < predictions.Count;

                return filteredPrediction;
            }

            for (int rowIndex = 0; rowIndex < rowsPredictions.Length; rowIndex++)
            {
                rowsPredictions[rowIndex] = FilterLine(rowsPredictions[rowIndex], field.GetRow(rowIndex));
            }

            for (int columnIndex = 0; columnIndex < columnsPredictions.Length; columnIndex++)
            {
                columnsPredictions[columnIndex] = FilterLine(columnsPredictions[columnIndex], field.GetColumn(columnIndex));
            }

            if (!anyChanged)
                return FilterResult.Unsolvable;

            return FilterResult.RequireMapping;
        }


        private enum FilterResult : byte
        {
            Unsolvable,
            RequireMapping
        }
    }

}
