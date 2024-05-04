namespace SchneiderElectric.MindSweeper.Columns;

internal sealed record LastColumn(Field.Columns Columns) : IHasColumnOnLeft
{
    public int Index => Columns.Length - 1;
}
