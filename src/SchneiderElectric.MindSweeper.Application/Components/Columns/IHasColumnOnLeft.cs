namespace SchneiderElectric.MindSweeper.Application.Components.Columns;

internal interface IHasColumnOnLeft : IColumn
{
    IColumn Left => Columns[Index - 1];
}
