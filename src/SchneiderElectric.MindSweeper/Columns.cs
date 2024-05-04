using SchneiderElectric.MindSweeper.Columns;
using SchneiderElectric.MindSweeper.Exceptions;

namespace SchneiderElectric.MindSweeper;

partial class Field
{
    public sealed class Columns
    {
        private readonly Lazy<IColumn[]> _columns;

        public Columns(int capacity)
        {
            if (capacity < Settings.MinimumColumns)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"The capacity setting for the number of columns must be at least {Settings.MinimumColumns}.");
            }

            if (capacity > Settings.MaximumColumns)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"The capacity setting for the number of columns must not exceed {Settings.MaximumColumns}.");
            }

            _columns = new(() =>
            {
                var columns = new IColumn[capacity];

                for (var i = 0; i < capacity; i++)
                {
                    columns[i] = i switch
                    {
                        0 => new FirstColumn(this),
                        _ when i == capacity - 1 => new LastColumn(this),
                        _ => new MiddleColumn(this, i)
                    };
                }

                return columns;
            });
        }

        public int Length => _columns.Value.Length;

        public IColumn this[int index] => index < 0 || index >= _columns.Value.Length
            ? throw new ColumnIndexOutOfRangeException() : _columns.Value[index];
    }
}
