namespace MindSweeper.Application.Components.Rows;

/// <summary>
/// Represents the first row of the field.
/// </summary>
internal sealed record FirstRow(Field.Rows Rows) : IHasRowAbove
{
    /// <summary>
    /// Gets the index of the first row.
    /// </summary>
    public int Index => 0;
}
