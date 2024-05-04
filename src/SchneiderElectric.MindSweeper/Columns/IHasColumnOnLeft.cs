namespace SchneiderElectric.MindSweeper.Columns;

internal interface IHasColumnOnLeft : IColumn
{
    IColumn Left => Columns[Index - 1];
}
