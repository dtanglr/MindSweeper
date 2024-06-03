using MindSweeper.Domain.Components.Columns;
using MindSweeper.Domain.Components.Rows;

namespace MindSweeper.Domain.Components.Squares;

/// <summary>
/// Represents a square in the MindSweeper game.
/// </summary>
internal interface ISquare
{
    /// <summary>
    /// Gets the column that the square belongs to.
    /// </summary>
    IColumn Column { get; init; }

    /// <summary>
    /// Gets the index of the square within its column.
    /// </summary>
    int Index { get; }

    /// <summary>
    /// Gets a value indicating whether the square is on the last row.
    /// </summary>
    bool IsOnLastRow { get; }

    /// <summary>
    /// Gets the name of the square.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the row that the square belongs to.
    /// </summary>
    IRow Row { get; init; }

    /// <summary>
    /// Gets or sets the squares field.
    /// </summary>
    Field.Squares Squares { get; init; }

    /// <summary>
    /// Gets the available moves for the square.
    /// </summary>
    /// <returns>A dictionary of available moves, where the key is the direction and the value is the move name.</returns>
    Dictionary<Direction, string> GetAvailableMoves();

    /// <summary>
    /// Checks if the square has a bomb.
    /// </summary>
    /// <param name="bombs">The bombs field.</param>
    /// <returns>True if the square has a bomb, otherwise false.</returns>
    bool HasBomb(Field.Bombs bombs);

    /// <summary>
    /// Tries to move the square in the specified direction.
    /// </summary>
    /// <param name="move">The direction to move.</param>
    /// <param name="square">When this method returns, contains the square that the square moved to, if the move was successful; otherwise, null.</param>
    /// <returns>True if the move was successful, otherwise false.</returns>
    bool TryMove(Direction move, out ISquare? square);
}
