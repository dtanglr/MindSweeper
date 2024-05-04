namespace SchneiderElectric.MindSweeper.Rows;

internal sealed record LastRow(Field.Rows Rows) : IHasRowBelow
{
    public int Index => Rows.Length - 1;
}
