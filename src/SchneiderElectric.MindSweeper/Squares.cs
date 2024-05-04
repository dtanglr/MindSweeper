using SchneiderElectric.MindSweeper.Exceptions;
using SchneiderElectric.MindSweeper.Squares;

namespace SchneiderElectric.MindSweeper;

partial class Field
{
    public sealed class Squares
    {
        private readonly Lazy<Dictionary<string, Square>> _squares;
        private readonly int _columnCapacity;

        public Squares(int columnCapacity, int rowCapacity)
        {
            _columnCapacity = columnCapacity;
            _squares = new(() =>
            {
                var columns = new Columns(columnCapacity);
                var rows = new Rows(rowCapacity);
                var squares = new Dictionary<string, Square>(columnCapacity * rowCapacity);

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
                    ? throw new SquareIndexOutOfRangeException() : _squares.Value.ElementAt(index).Value;
            }
        }

        public Square this[string name]
        {
            get
            {
                return !_squares.Value.TryGetValue(name, out var square)
                    ? throw new SquareIndexOutOfRangeException() : square;
            }
        }

        public Square GetStartSquare()
        {
            var randomFirstRowSquareIndex = Random.Shared.Next(0, _columnCapacity - 1);
            return _squares.Value.ElementAt(randomFirstRowSquareIndex).Value;
        }
    }
}

