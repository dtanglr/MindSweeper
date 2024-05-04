using SchneiderElectric.MindSweeper.Columns;
using SchneiderElectric.MindSweeper.Moves;
using SchneiderElectric.MindSweeper.Rows;

namespace SchneiderElectric.MindSweeper.Squares;

internal record Square(Field.Squares Squares, IColumn Column, IRow Row)
{
    public int Index => (Column.Columns.Length * Row.Index) + Column.Index;

    public string Name => $"{Column.Name}{Row.Name}";

    public bool HasBomb(Field.Bombs bombs) => bombs.OnSquare(this);

    public bool TryMove(Move move, out Square? square)
    {
        square = move switch
        {
            Move.Up => Row is IHasRowAbove ? Squares[Index + Column.Columns.Length] : null,
            Move.Down => Row is IHasRowBelow ? Squares[Index - Column.Columns.Length] : null,
            Move.Left => Column is IHasColumnOnLeft ? Squares[Index - 1] : null,
            Move.Right => Column is IHasColumnOnRight ? Squares[Index + 1] : null,
            _ => null
        };

        return square is not null;
    }

    public Dictionary<Move, Square> GetAvailableMoves()
    {
        var moves = new Dictionary<Move, Square>();

        if (Row is IHasRowAbove)
        {
            moves.Add(Move.Up, Squares[Index + Column.Columns.Length]);
        }

        if (Row is IHasRowBelow)
        {
            moves.Add(Move.Down, Squares[Index - Column.Columns.Length]);
        }

        if (Column is IHasColumnOnLeft)
        {
            moves.Add(Move.Left, Squares[Index - 1]);
        }

        if (Column is IHasColumnOnRight)
        {
            moves.Add(Move.Right, Squares[Index + 1]);
        }

        return moves;
    }
}
