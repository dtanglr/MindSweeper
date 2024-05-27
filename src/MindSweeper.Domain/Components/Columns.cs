using MindSweeper.Domain.Components.Columns;
using MindSweeper.Domain.Exceptions;

namespace MindSweeper.Domain.Components;

partial class Field
{
    /// <summary>
    /// Represents the columns of the field.
    /// </summary>
    public sealed class Columns
    {
        private readonly Lazy<IColumn[]> _columns;

        /// <summary>
        /// Initializes a new instance of the <see cref="Columns"/> class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The number of columns.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the capacity is less than the minimum or exceeds the maximum number of columns.</exception>
        public Columns(int capacity)
        {
            if (capacity < Settings.MinimumColumns)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"The number of columns must be at least {Settings.MinimumColumns}.");
            }

            if (capacity > Settings.MaximumColumns)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"The number of columns must not exceed {Settings.MaximumColumns}.");
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

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public int Length => _columns.Value.Length;

        /// <summary>
        /// Gets the column at the specified index.
        /// </summary>
        /// <param name="index">The index of the column.</param>
        /// <returns>The column at the specified index.</returns>
        /// <exception cref="ColumnIndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public IColumn this[int index] => index < 0 || index >= _columns.Value.Length
            ? throw new ColumnIndexOutOfRangeException($"The column index of {index} was out of range.")
            : _columns.Value[index];
    }
}
