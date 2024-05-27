namespace MindSweeper.Domain.Components.Columns;

/// <summary>
/// Represents an interface for a column that has a column on the left.
/// </summary>
internal interface IHasColumnOnLeft : IColumn
{
    /// <summary>
    /// Gets the column on the left of the current column.
    /// </summary>
    IColumn Left => Columns[Index - 1];
}
