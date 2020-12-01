using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nono.Engine.Helpers;

namespace Nono.Engine
{
    public abstract class Line : IEnumerable<Box>
    {
        public Line(IEnumerable<Box> boxes)
        {
            Boxes = boxes.ToArray();
        }

        protected Box[] Boxes { get; }

        public int Length => Boxes.Length;

        public Box this[int i] => Boxes[i];

        public IEnumerator<Box> GetEnumerator() => ((IEnumerable<Box>)Boxes).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => GraphicsHelper.Map(this);

        public static implicit operator ReadOnlySpan<Box>(Line line) 
            => new ReadOnlySpan<Box>(line.Boxes);
    }
}