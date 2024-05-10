namespace SchneiderElectric.MindSweeper;

public class Settings
{
    public const string ConfigurationSectionKey = "MindGame";
    public const string ColumnNames = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const int MinimumColumns = 3;
    public const int DefaultColumns = 8;
    public const int MinimumRows = 3;
    public const int DefaultRows = 8;
    public const int MinimumBombs = 1;
    public const int DefaultBombs = 12;
    public const int MinimumLives = 1;
    public const int DefaultLives = 1;

    public readonly static int MaximumColumns = ColumnNames.Length;

    public int Columns { get; set; } = DefaultColumns;
    public int Rows { get; set; } = DefaultRows;
    public int Bombs { get; set; } = DefaultBombs;
    public int Lives { get; set; } = DefaultLives;
}
