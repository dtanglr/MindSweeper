namespace MindSweeper.Application.Components.Columns;

/// <summary>
/// Represents an interface for a column that has a column on the right.
/// </summary>
internal interface IHasColumnOnRight : IColumn
{
    /// <summary>
    /// Gets the column on the right.
    /// </summary>
    IColumn Right => Columns[Index + 1];
}
