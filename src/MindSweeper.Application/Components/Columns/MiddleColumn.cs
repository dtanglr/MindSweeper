namespace MindSweeper.Application.Components.Columns;

/// <summary>
/// Represents a middle column in the field.
/// </summary>
internal sealed record MiddleColumn(Field.Columns Columns, int Index) : IHasColumnOnLeft, IHasColumnOnRight;
