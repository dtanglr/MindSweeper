using SchneiderElectric.MindSweeper.Application.Components.Squares;
using SchneiderElectric.MindSweeper.Application.Exceptions;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Components;

partial class Field
{
    public sealed class Squares
    {
        private readonly Lazy<Dictionary<string, Square>> _squares;
        private readonly int _columnCapacity;

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

        public int Length => _squares.Value.Count;

        public Square this[int index]
        {
            get
            {
                return index < 0 || index >= _squares.Value.Count
                    ? throw new SquareIndexOutOfRangeException($"The square index of {index} was out of range.") : _squares.Value.ElementAt(index).Value;
            }
        }

        public Square this[string index]
        {
            get
            {
                return !_squares.Value.TryGetValue(index, out var square)
                    ? throw new SquareIndexOutOfRangeException($"The square index of {index} was out of range.") : square;
            }
        }

        public Square GetStartSquare()
        {
            var randomFirstRowSquareIndex = Random.Shared.Next(0, _columnCapacity - 1);
            return _squares.Value.ElementAt(randomFirstRowSquareIndex).Value;
        }
    }
}

