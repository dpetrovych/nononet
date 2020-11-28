﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nono.Engine.B.Helpers;

namespace Nono.Engine.B
{
    public class Field : IEnumerable<Box>
    {
        private readonly Box[] _field;

        public Field(int rowCount, int columnCount)
        {
            if (rowCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(rowCount));

            if (columnCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(columnCount));

            RowCount = rowCount;
            ColumnCount = columnCount;

            _field = new Box[rowCount * columnCount];
            Array.Fill(_field, Box.Empty);
        }

        public int RowCount { get; }

        public int ColumnCount { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetPosition(int rowIndex, int columnIndex)
        {
            unchecked
            {
                return rowIndex * ColumnCount + columnIndex;
            }
        }

        private Func<int, int> GetRowIndexer(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowCount)
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            return columnIndex => GetPosition(rowIndex, columnIndex);
        }

        private Func<int, int> GetColumnIndexer(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnCount)
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            return rowIndex => GetPosition(rowIndex, columnIndex);
        }

        public IEnumerable<Box> GetRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowCount)
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            for (int i = 0; i < ColumnCount; i++)
                yield return _field[GetPosition(rowIndex, i)];
        }

        public IEnumerable<Box> GetColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnCount)
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            for (int i = 0; i < RowCount; i++)
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

        public Func<int, int> GetLineIndexer(LineIndex index)
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
                _field[fieldIndexer(i)] = line[i];
        }

        public IEnumerator<Box> GetEnumerator()
            => ((IEnumerable<Box>)_field).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public override string ToString() => GraphicsHelper.Map(this);
    }
}