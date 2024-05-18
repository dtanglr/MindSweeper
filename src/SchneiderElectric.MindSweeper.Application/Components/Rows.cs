using SchneiderElectric.MindSweeper.Application.Components.Rows;
using SchneiderElectric.MindSweeper.Application.Exceptions;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Components;

partial class Field
{
    public sealed class Rows
    {
        private readonly Lazy<IRow[]> _rows;

        public Rows(int capacity)
        {
            if (capacity < Settings.MinimumRows)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"The number of rows must be at least {Settings.MinimumRows}.");
            }

            if (capacity > Settings.MaximumRows)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"The number of rows must not exceed {Settings.MaximumRows}.");
            }

            _rows = new(() =>
            {
                var rows = new IRow[capacity];

                for (var i = 0; i < capacity; i++)
                {
                    rows[i] = i switch
                    {
                        0 => new FirstRow(this),
                        _ when i == capacity - 1 => new LastRow(this),
                        _ => new MiddleRow(this, i)
                    };
                }

                return rows;
            });
        }

        public int Length => _rows.Value.Length;

        public IRow this[int index]
        {
            get
            {
                return index < 0 || index >= _rows.Value.Length
                    ? throw new RowIndexOutOfRangeException($"The row index of {index} was out of range.") : _rows.Value[index];
            }
        }
    }
}
