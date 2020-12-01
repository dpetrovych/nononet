using System.Collections.Generic;
using System.Linq;

namespace Nono.Engine
{
    public ref struct CollapseCollector
    {
        private Box[]? _boxes;

        private decimal _combinationsCount;

        public void Add(IEnumerable<Box> line, decimal combinationsCount)
        {
            if (_boxes == null) 
            {
                _boxes = line.ToArray();
                _combinationsCount = combinationsCount;
                return;
            }

            using (IEnumerator<Box> lineEnum = line.GetEnumerator())
            {
                int i = -1;
                while (++i < _boxes.Length && lineEnum.MoveNext())
                {
                    if (_boxes[i] != lineEnum.Current)
                        _boxes[i] = Box.Empty;
                }
            }

            _combinationsCount += combinationsCount;
        }

        public CollapseLine ToCollapseLine() 
            => _boxes != null 
                ? new CollapseLine(_boxes, _combinationsCount) 
                : new CollapseLine();
    }
}