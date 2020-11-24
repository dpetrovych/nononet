using System.Collections;
using System.Collections.Generic;
using Nono.Engine.Helpers;

namespace Nono.Engine
{
    public struct Line : IReadOnlyList<Box>
    {
        private readonly Box[] _boxes;

        public Line(Box[] boxes)
        {
            _boxes = boxes;
        }

        public Box this[int i] => _boxes[i];

        public IEnumerator<Box> GetEnumerator() => ((IEnumerable<Box>)_boxes).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _boxes.Length;

        public override string ToString() => GraphicsHelper.Map(this);

        public Box[] ToBoxArray() => (Box[]) _boxes.Clone();
    }
}