namespace MindSweeper.Application.Components.Rows;

internal interface IHasRowAbove : IRow
{
    IRow Above => Rows[Index + 1];
}
