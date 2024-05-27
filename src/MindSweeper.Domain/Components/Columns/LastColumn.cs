namespace MindSweeper.Domain.Components.Columns;

/// <summary>
/// Represents the last column in a field.
/// </summary>
internal sealed record LastColumn(Field.Columns Columns) : IHasColumnOnLeft
{
    /// <summary>
    /// Gets the index of the last column.
    /// </summary>
    public int Index => Columns.Length - 1;
}
