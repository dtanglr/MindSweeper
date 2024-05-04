namespace SchneiderElectric.MindSweeper.Columns;

internal interface IColumn
{
    int Index { get; }

    char Name => Settings.ColumnNames[Index];

    Field.Columns Columns { get; }
}
