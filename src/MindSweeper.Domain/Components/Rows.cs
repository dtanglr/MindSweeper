﻿using MindSweeper.Domain.Components.Rows;
using MindSweeper.Domain.Exceptions;

namespace MindSweeper.Domain.Components;

partial class Field
{
    /// <summary>
    /// Represents the collection of rows in the field.
    /// </summary>
    public sealed class Rows
    {
        private readonly Lazy<IRow[]> _rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rows"/> class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The number of rows in the field.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the capacity is less than the minimum number of rows or exceeds the maximum number of rows.</exception>
        public Rows(int capacity)
        {
            if (capacity < GameSettings.MinimumRows)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"The number of rows must be at least {GameSettings.MinimumRows}.");
            }

            if (capacity > GameSettings.MaximumRows)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), $"The number of rows must not exceed {GameSettings.MaximumRows}.");
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

        /// <summary>
        /// Gets the number of rows in the field.
        /// </summary>
        public int Length => _rows.Value.Length;

        /// <summary>
        /// Gets the row at the specified index.
        /// </summary>
        /// <param name="index">The index of the row.</param>
        /// <returns>The row at the specified index.</returns>
        /// <exception cref="RowIndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public IRow this[int index]
        {
            get
            {
                return index < 0 || index >= _rows.Value.Length
                    ? throw new RowIndexOutOfRangeException($"The row index of {index} was out of range.")
                    : _rows.Value[index];
            }
        }
    }
}
