using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nono.Engine.Helpers;

namespace Nono.Engine
{
    public class Field : IEnumerable<Box>
    {
        private readonly Box[] _field;

        public Field(ushort rowCount, ushort columnCount)
        {
            if (rowCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(rowCount));

            if (columnCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(columnCount));

            RowCount = rowCount;
            RowSize = ColumnCount = columnCount;
            
            _field = (Box[])Array.CreateInstance(typeof(Box), unchecked(RowCount * RowSize));
            Array.Fill(_field, Box.Empty);
        }

        public ushort RowCount { get; }

        public ushort ColumnCount { get; }

        private long RowSize { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long GetPosition(ushort rowIndex, ushort columnIndex)
            => unchecked(rowIndex * RowSize + columnIndex);

        private Func<ushort, long> GetRowIndexer(ushort rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowCount)
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            return columnIndex => GetPosition(rowIndex, columnIndex);
        }

        private Func<ushort, long> GetColumnIndexer(ushort columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnCount)
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            return rowIndex => GetPosition(rowIndex, columnIndex);
        }

        public IEnumerable<Box> GetRow(ushort rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowCount)
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            for (ushort i = 0; i < ColumnCount; i++)
                yield return _field[GetPosition(rowIndex, i)];
        }

        public IEnumerable<Box> GetColumn(ushort columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnCount)
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            for (ushort i = 0; i < RowCount; i++)
                yield return _field[GetPosition(i, columnIndex)];
        }

        public FieldLine GetLine(LineIndex index)
        {
            var indexer = GetLineIndexer(index);

            switch (index.Orienation)
            {
                case Orientation.Row:
                    return new FieldLine(GetRow(index.Position), index);
                case Orientation.Column:
                    return new FieldLine(GetColumn(index.Position), index);
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public Func<ushort, long> GetLineIndexer(LineIndex index)
        {
            switch (index.Orienation)
            {
                case Orientation.Row:
                    return GetRowIndexer(index.Position);
                case Orientation.Column:
                    return GetColumnIndexer(index.Position);
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public void Set(DiffLine line)
        {
            var fieldIndexer = GetLineIndexer(line.Index);
            foreach (var i in line.NonEmptyIndexes())
                _field.SetValue(line[i], fieldIndexer(i));
        }

        public IEnumerator<Box> GetEnumerator()
            => ((IEnumerable<Box>)_field).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public override string ToString() => GraphicsHelper.Map(this);
    }
}