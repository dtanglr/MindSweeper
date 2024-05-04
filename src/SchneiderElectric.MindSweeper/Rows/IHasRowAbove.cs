namespace SchneiderElectric.MindSweeper.Rows;

internal interface IHasRowAbove : IRow
{
    IRow Above => Rows[Index + 1];
}
