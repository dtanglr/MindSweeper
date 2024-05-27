namespace MindSweeper.Domain.Components.Rows;

/// <summary>
/// Represents an interface for a row that has a row above.
/// </summary>
internal interface IHasRowBelow : IRow
{
    /// <summary>
    /// Gets the row below the current row.
    /// </summary>
    IRow Below => Rows[Index - 1];
}
