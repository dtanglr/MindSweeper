namespace MindSweeper.Application.Components.Rows;

/// <summary>
/// Represents a middle row in the field.
/// </summary>
internal sealed record MiddleRow(Field.Rows Rows, int Index) : IHasRowAbove, IHasRowBelow;
