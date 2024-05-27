namespace MindSweeper.Domain.Components.Rows;

/// <summary>
/// Represents a row in the MindSweeper game.
/// </summary>
internal interface IRow
{
    /// <summary>
    /// Gets the index of the row.
    /// </summary>
    int Index { get; }

    /// <summary>
    /// Gets the name of the row.
    /// </summary>
    string Name => $"{Index + 1}";

    /// <summary>
    /// Gets the collection of fields in the row.
    /// </summary>
    Field.Rows Rows { get; }
}
