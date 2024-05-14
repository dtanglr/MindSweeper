using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Components.Columns;

internal interface IColumn
{
    int Index { get; }

    char Name => Settings.ColumnNames[Index];

    Field.Columns Columns { get; }
}
