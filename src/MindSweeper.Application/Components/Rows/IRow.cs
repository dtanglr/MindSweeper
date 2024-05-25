namespace MindSweeper.Application.Components.Rows;

internal interface IRow
{
    int Index { get; }

    string Name => $"{Index + 1}";

    Field.Rows Rows { get; }
}
