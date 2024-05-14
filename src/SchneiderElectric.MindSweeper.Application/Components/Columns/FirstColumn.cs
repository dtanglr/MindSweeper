namespace SchneiderElectric.MindSweeper.Application.Components.Columns;

internal sealed record FirstColumn(Field.Columns Columns) : IHasColumnOnRight
{
    public int Index => 0;
}
