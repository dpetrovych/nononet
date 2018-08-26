using System.Collections.Generic;

namespace Nono.Engine.Models
{
    public class Solution
    {
        private readonly List<Field> _steps = new List<Field>();

        public Field Field { get; private set; }

        public IReadOnlyCollection<Field> Steps => _steps;

        public void AddStep(Field field)
        {
            _steps.Add(field);
        }

        public void AddResult(Field field)
        {
            _steps.Add(field);
            Field = field;
        }
    }
}