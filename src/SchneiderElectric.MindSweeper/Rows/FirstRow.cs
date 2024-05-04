namespace SchneiderElectric.MindSweeper.Rows;

internal sealed record FirstRow(Field.Rows Rows) : IHasRowAbove
{
    public int Index => 0;
}
