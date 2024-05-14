namespace SchneiderElectric.MindSweeper.Application.Components.Rows;

internal sealed record MiddleRow(Field.Rows Rows, int Index) : IHasRowAbove, IHasRowBelow;
