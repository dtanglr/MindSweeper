namespace SchneiderElectric.MindSweeper.Rows;

internal interface IHasRowBelow : IRow
{
    IRow Below => Rows[Index - 1];
}
