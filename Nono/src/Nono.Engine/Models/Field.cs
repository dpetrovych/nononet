using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Nono.Engine.Models
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
        }

        public Field(Box[] cells, int rowCount, int columnCount)
        {
            if (cells == null)
                throw new ArgumentNullException(nameof(cells));

            if (rowCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(rowCount));

            if (columnCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(columnCount));

            if (cells.Length != rowCount * columnCount)
                throw new ArgumentException($"Cell array should be {rowCount}x{columnCount} in single dimention", nameof(cells));

            RowCount = rowCount;
            ColumnCount = columnCount;

            _field = (Box[])cells.Clone();
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

        public IEnumerable<Cell> GetRowEnumerator(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowCount)
                throw new ArgumentOutOfRangeException(nameof(rowIndex));
            
            for (int i = 0; i < ColumnCount; i++)
                yield return new Cell(_field, GetPosition(rowIndex, i), i);
        }

        public IEnumerable<Cell> GetColumnEnumerator(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnCount)
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            for (int i = 0; i < RowCount; i++)
                yield return new Cell(_field, GetPosition(i, columnIndex), i);
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

        public IEnumerator<Box> GetEnumerator() => ((IEnumerable<Box>)_field).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>  GetEnumerator();

        public override bool Equals(object obj)
        {
            var field = obj as Field;
            return field != null &&
                   RowCount == field.RowCount &&
                   ColumnCount == field.ColumnCount &&
                   _field.SequenceEqual(field._field);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_field, RowCount, ColumnCount);
        }
    }
    
    public struct Cell
    {
        private readonly Box[] _field;
        private readonly int _position;

        public Cell(Box[] field, int position, int index)
        {
            _field = field;
            _position = position;
            Index = index;
        }

        public int Index { get; }

        public static implicit operator Box(Cell cell)
        {
            return cell._field[cell._position];
        }

        public void Set(Box value)
        {
            _field[_position] = value;
        }
    }
}