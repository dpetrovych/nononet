using System.Collections.Generic;
using System.Linq;
using Nono.Engine.Extensions;

namespace Nono.Engine
{
    public class Solve
    {
        public Box[,] Run(IEnumerable<uint[]> rows, IEnumerable<uint[]> columns)
        {
            var rowsDef = rows.ToList();
            var columnsDef = columns.ToList();

            var field = new Box[rowsDef.Count, columnsDef.Count];

            var rowsPredictions = rowsDef.Select(row => LinePrediction.Generate(row, columnsDef.Count)).ToArray();
            var columnsPredictions = columnsDef.Select(column => LinePrediction.Generate(column, rowsDef.Count)).ToArray();

            while (Predict(rowsPredictions, columnsPredictions, out var nextField))
            {
                
            }

            return field;
        }

        private static bool Predict(Box[][][] rowsPredictions, Box[][][] columnsPredictions, out Box[,] field)
        {
            field = new Box[rowsPredictions.Length, columnsPredictions.Length];

            for (var rowIndex = 0; rowIndex < rowsPredictions.Length; rowIndex++)
            {
                var currentPrediction = PredictLine(rowsPredictions[rowIndex]);

                for (int columnIndex = 0; columnIndex < currentPrediction.Length; columnIndex++)
                {
                    if (field[rowIndex, columnIndex] == Box.Empty)
                        field[rowIndex, columnIndex] = currentPrediction[columnIndex];
                }
            }

            for (var columnIndex = 0; columnIndex < columnsPredictions.Length; columnIndex++)
            {
                var currentPrediction = PredictLine(columnsPredictions[columnIndex]);

                for (int rowIndex = 0; rowIndex < currentPrediction.Length; rowIndex++)
                {
                    if (field[rowIndex, columnIndex] == Box.Empty)
                        field[rowIndex, columnIndex] = currentPrediction[columnIndex];
                }
            }

            return field.Cast<Box>().All(x => x != Box.Empty);
        }

        private static Box[] PredictLine(Box[][] currentPredictions)
        {
            var currentPrediction = (Box[]) currentPredictions[0].Clone();

            foreach (var nextPrediction in currentPredictions.Skip(1))
            {
                for (int i = 0; i < currentPrediction.Length; i++)
                {
                    if (currentPrediction[i] != Box.Empty && currentPrediction[i] != nextPrediction[i])
                        currentPrediction[i] = Box.Empty;
                }
            }

            return currentPrediction;
        }
    }
}
