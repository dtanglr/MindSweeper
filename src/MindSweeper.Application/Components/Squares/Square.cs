using MindSweeper.Application.Components.Columns;
using MindSweeper.Application.Components.Rows;
using MindSweeper.Domain;

namespace MindSweeper.Application.Components.Squares;

internal record Square(Field.Squares Squares, IColumn Column, IRow Row)
{
    public int Index => (Column.Columns.Length * Row.Index) + Column.Index;

    public string Name => $"{Column.Name}{Row.Name}";

    public bool HasBomb(Field.Bombs bombs) => bombs.OnSquare(this);

    public bool TryMove(Direction move, out Square? square)
    {
        square = move switch
        {
            Direction.Up when Row is IHasRowAbove => Squares[Index + Column.Columns.Length],
            Direction.Down when Row is IHasRowBelow => Squares[Index - Column.Columns.Length],
            Direction.Left when Column is IHasColumnOnLeft => Squares[Index - 1],
            Direction.Right when Column is IHasColumnOnRight => Squares[Index + 1],
            _ => null
        };

        return square is not null;
    }

    public Dictionary<Direction, string> GetAvailableMoves()
    {
        var moves = new Dictionary<Direction, string>();

        if (Row is IHasRowAbove)
        {
            moves.Add(Direction.Up, Squares[Index + Column.Columns.Length].Name);
        }

        if (Row is IHasRowBelow)
        {
            moves.Add(Direction.Down, Squares[Index - Column.Columns.Length].Name);
        }

        if (Column is IHasColumnOnLeft)
        {
            moves.Add(Direction.Left, Squares[Index - 1].Name);
        }

        if (Column is IHasColumnOnRight)
        {
            moves.Add(Direction.Right, Squares[Index + 1].Name);
        }

        return moves;
    }
}
