namespace SchneiderElectric.MindSweeper;

public class Settings
{
    public const string ConfigurationSectionKey = "MindGame";
    public const string ColumnNames = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const int MinimumColumns = 3;
    public const int MinimumRows = 3;
    public const int MinimumBombs = 1;
    public const int MinimumLives = 1;

    public readonly static int MaximumColumns = ColumnNames.Length;

    public int Columns { get; set; } = 8;
    public int Rows { get; set; } = 8;
    public int Bombs { get; set; } = 12;
    public int Lives { get; set; } = 3;
}
