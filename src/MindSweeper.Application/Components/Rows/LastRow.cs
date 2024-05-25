namespace MindSweeper.Application.Components.Rows;

internal sealed record LastRow(Field.Rows Rows) : IHasRowBelow
{
    public int Index => Rows.Length - 1;
}
