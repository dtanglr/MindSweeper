namespace MindSweeper.Domain.Components.Rows;

/// <summary>
/// Represents an interface for a row that has a row below.
/// </summary>
internal interface IHasRowAbove : IRow
{
    /// <summary>
    /// Gets the row above the current row.
    /// </summary>
    IRow Above => Rows[Index + 1];
}
