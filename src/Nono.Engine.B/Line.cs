using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nono.Engine.B.Helpers;

namespace Nono.Engine.B
{
    public abstract class Line : IEnumerable<Box>
    {
        private readonly Box[] _boxes;

        public Line(IEnumerable<Box> boxes)
        {
            _boxes = boxes.ToArray();
        }

        public Box this[int i] => _boxes[i];

        public IEnumerator<Box> GetEnumerator() => ((IEnumerable<Box>)_boxes).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Length => _boxes.Length;

        public override string ToString() => GraphicsHelper.Map(this);

        public static implicit operator ReadOnlySpan<Box>(Line line) 
            => new ReadOnlySpan<Box>(line._boxes);
    }
}