namespace MindSweeper.Application.Components.Columns;

internal interface IHasColumnOnRight : IColumn
{
    IColumn Right => Columns[Index + 1];
}
