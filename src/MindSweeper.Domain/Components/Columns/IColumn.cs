namespace MindSweeper.Domain.Components.Columns;

/// <summary>
/// Represents a column in the MindSweeper game.
/// </summary>
internal interface IColumn
{
    /// <summary>
    /// Gets the index of the column.
    /// </summary>
    int Index { get; }

    /// <summary>
    /// Gets the name of the column.
    /// </summary>
    char Name => Settings.ColumnNames[Index];

    /// <summary>
    /// Gets the columns of the field.
    /// </summary>
    Field.Columns Columns { get; }
}
