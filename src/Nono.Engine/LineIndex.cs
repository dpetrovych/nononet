using System;

namespace Nono.Engine
{
    public enum Orientation
    {
        Row = 0,
        Column = 1,
    }

    public static class OrientationExtensions
    {
        public static Orientation Opposite(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Row: return Orientation.Column;
                case Orientation.Column: return Orientation.Row;
            }

            throw new ArgumentOutOfRangeException(nameof(orientation));
        }

        public static char Code(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Row: return 'r';
                case Orientation.Column: return 'c';
            }

            throw new ArgumentOutOfRangeException(nameof(orientation));
        }
    }

    public record LineIndex
    {
        public Orientation Orienation { get; }

        public int Position { get; }

        public LineIndex(Orientation orienation, int position)
            => (Orienation, Position) = (orienation, position);

        public override string ToString()
            => $"{Orienation.Code()}{Position}";
    }
}