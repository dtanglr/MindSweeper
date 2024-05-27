namespace MindSweeper.Application.Components.Rows;

/// <summary>
/// Represents the last row in the field.
/// </summary>
internal sealed record LastRow(Field.Rows Rows) : IHasRowBelow
{
    /// <summary>
    /// Gets the index of the last row.
    /// </summary>
    public int Index => Rows.Length - 1;
}
