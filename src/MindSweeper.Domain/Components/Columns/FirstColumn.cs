namespace MindSweeper.Domain.Components.Columns;

/// <summary>
/// Represents the first column in the field.
/// </summary>
internal sealed record FirstColumn(Field.Columns Columns) : IHasColumnOnRight
{
    /// <summary>
    /// Gets the index of the first column.
    /// </summary>
    public int Index => 0;
}
