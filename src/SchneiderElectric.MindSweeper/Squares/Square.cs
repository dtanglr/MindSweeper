using SchneiderElectric.MindSweeper.Columns;
using SchneiderElectric.MindSweeper.Rows;

namespace SchneiderElectric.MindSweeper.Squares;

internal record Square(Field.Squares Squares, IColumn Column, IRow Row)
{
    public int Index => (Column.Columns.Length * Row.Index) + Column.Index;

    public string Name => $"{Column.Name}{Row.Name}";

    public bool HasBomb(Field.Bombs bombs) => bombs.OnSquare(this);

    public bool TryMove(Direction move, out Square? square)
    {
        square = move switch
        {
            Direction.Up => Row is IHasRowAbove ? Squares[Index + Column.Columns.Length] : null,
            Direction.Down => Row is IHasRowBelow ? Squares[Index - Column.Columns.Length] : null,
            Direction.Left => Column is IHasColumnOnLeft ? Squares[Index - 1] : null,
            Direction.Right => Column is IHasColumnOnRight ? Squares[Index + 1] : null,
            _ => null
        };

        return square is not null;
    }

    public Dictionary<Direction, Square> GetAvailableMoves()
    {
        var moves = new Dictionary<Direction, Square>();

        if (Row is IHasRowAbove)
        {
            moves.Add(Direction.Up, Squares[Index + Column.Columns.Length]);
        }

        if (Row is IHasRowBelow)
        {
            moves.Add(Direction.Down, Squares[Index - Column.Columns.Length]);
        }

        if (Column is IHasColumnOnLeft)
        {
            moves.Add(Direction.Left, Squares[Index - 1]);
        }

        if (Column is IHasColumnOnRight)
        {
            moves.Add(Direction.Right, Squares[Index + 1]);
        }

        return moves;
    }
}
