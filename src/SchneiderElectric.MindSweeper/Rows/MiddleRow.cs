namespace SchneiderElectric.MindSweeper.Rows;

internal sealed record MiddleRow(Field.Rows Rows, int Index) : IHasRowAbove, IHasRowBelow;
