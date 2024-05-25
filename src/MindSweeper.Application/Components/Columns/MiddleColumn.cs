namespace MindSweeper.Application.Components.Columns;

internal sealed record MiddleColumn(Field.Columns Columns, int Index) : IHasColumnOnLeft, IHasColumnOnRight;
