using MindSweeper.Domain.Components.Squares;
using MindSweeper.Domain.Exceptions;

namespace MindSweeper.Domain.Components;

partial class Field
{
    /// <summary>
    /// Represents the collection of squares in the field.
    /// </summary>
    public sealed class Squares
    {
        private readonly Lazy<Dictionary<string, Square>> _squares;
        private readonly int _columnCapacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="Squares"/> class.
        /// </summary>
        /// <param name="settings">The game settings.</param>
        public Squares(Settings settings) : this(settings.Columns, settings.Rows, settings.Squares) { }

        private Squares(int columnCapacity, int rowCapacity, int squaresCapacity)
        {
            _columnCapacity = columnCapacity;
            _squares = new(() =>
            {
                var columns = new Columns(columnCapacity);
                var rows = new Rows(rowCapacity);
                var squares = new Dictionary<string, Square>(squaresCapacity);

                for (var i = 0; i < rows.Length; i++)
                {
                    for (var j = 0; j < columns.Length; j++)
                    {
                        var square = new Square(this, columns[j], rows[i]);
                        squares[square.Name] = square;
                    }
                }

                return squares;
            });
        }

        /// <summary>
        /// Gets the number of squares in the collection.
        /// </summary>
        public int Length => _squares.Value.Count;

        /// <summary>
        /// Gets the square at the specified index.
        /// </summary>
        /// <param name="index">The index of the square.</param>
        /// <returns>The square at the specified index.</returns>
        /// <exception cref="SquareIndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public Square this[int index]
        {
            get
            {
                return index < 0 || index >= _squares.Value.Count
                    ? throw new SquareIndexOutOfRangeException($"The square index of {index} was out of range.")
                    : _squares.Value.ElementAt(index).Value;
            }
        }

        /// <summary>
        /// Gets the square with the specified name.
        /// </summary>
        /// <param name="index">The name of the square.</param>
        /// <returns>The square with the specified name.</returns>
        /// <exception cref="SquareIndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public Square this[string index]
        {
            get
            {
                return !_squares.Value.TryGetValue(index, out var square)
                    ? throw new SquareIndexOutOfRangeException($"The square index of {index} was out of range.")
                    : square;
            }
        }

        /// <summary>
        /// Gets a random square from the first row.
        /// </summary>
        /// <returns>A random square from the first row.</returns>
        public Square GetStartSquare()
        {
            var randomFirstRowSquareIndex = Random.Shared.Next(0, _columnCapacity - 1);
            return _squares.Value.ElementAt(randomFirstRowSquareIndex).Value;
        }
    }
}

