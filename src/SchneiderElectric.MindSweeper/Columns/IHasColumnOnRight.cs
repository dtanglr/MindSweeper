namespace SchneiderElectric.MindSweeper.Columns;

internal interface IHasColumnOnRight : IColumn
{
    IColumn Right => Columns[Index + 1];
}
