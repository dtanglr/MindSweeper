namespace SchneiderElectric.MindSweeper.Application.Components.Rows;

internal interface IHasRowBelow : IRow
{
    IRow Below => Rows[Index - 1];
}
