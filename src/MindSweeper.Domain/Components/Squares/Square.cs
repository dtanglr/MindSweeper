using MindSweeper.Domain.Components.Columns;
using MindSweeper.Domain.Components.Rows;

namespace MindSweeper.Domain.Components.Squares;

/// <summary>
/// Represents a square in the field.
/// </summary>
internal sealed record Square(Field.Squares Squares, IColumn Column, IRow Row) : ISquare
{
    /// <summary>
    /// Gets the index of the square.
    /// </summary>
    public int Index => (Column.Columns.Length * Row.Index) + Column.Index;

    /// <summary>
    /// Gets the name of the square.
    /// </summary>
    public string Name => $"{Column.Name}{Row.Name}";

    /// <summary>
    /// Determines if the square is on the last row.
    /// </summary>
    public bool IsOnLastRow => Row is not IHasRowAbove;

    /// <summary>
    /// Checks if the square has a bomb.
    /// </summary>
    /// <param name="bombs">The bombs in the field.</param>
    /// <returns>True if the square has a bomb, otherwise false.</returns>
    public bool HasBomb(Field.Bombs bombs) => bombs.OnSquare(this);

    /// <summary>
    /// Tries to move to a neighboring square in the specified direction.
    /// </summary>
    /// <param name="move">The direction to move.</param>
    /// <param name="square">The neighboring square, if the move is successful.</param>
    /// <returns>True if the move is successful, otherwise false.</returns>
    public bool TryMove(Direction move, out ISquare? square)
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

    /// <summary>
    /// Gets the available moves from the current square.
    /// </summary>
    /// <returns>A dictionary of available moves and their corresponding square names.</returns>
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
