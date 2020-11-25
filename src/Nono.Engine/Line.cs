using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nono.Engine.Helpers;

namespace Nono.Engine
{
    public abstract class Line : IReadOnlyList<Box>
    {
        private readonly Box[] _boxes;

        public Line(IEnumerable<Box> boxes)
        {
            _boxes = boxes.ToArray();
        }

        public Box this[int i] => _boxes[i];

        public IEnumerator<Box> GetEnumerator() => ((IEnumerable<Box>)_boxes).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _boxes.Length;

        public override string ToString() => GraphicsHelper.Map(this);

        public ReadOnlySpan<Box> AsSpan() => new ReadOnlySpan<Box>(_boxes);

        public ReadOnlySpan<Box> Slice(int start, int length) => new ReadOnlySpan<Box>(_boxes, start, length);
    }
}