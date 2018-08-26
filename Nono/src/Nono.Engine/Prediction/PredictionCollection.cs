using System.Collections.Generic;

namespace Nono.Engine.Prediction
{
    public class PredictionCollection : List<Line>
    {
        public PredictionCollection()
        {
        }

        public PredictionCollection(IEnumerable<Line> enumerable)
            : base(enumerable)
        {
        }
    }
}