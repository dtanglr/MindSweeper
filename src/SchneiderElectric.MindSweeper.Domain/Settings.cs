namespace SchneiderElectric.MindSweeper.Domain;

public class Settings
{
    public const string ColumnNames = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const int MinimumColumns = 3;
    public const int MaximumColumns = 26;
    public const int DefaultColumns = 8;
    public const int MinimumRows = 3;
    public const int MaximumRows = 26;
    public const int DefaultRows = 8;
    public const int MinimumBombs = 1;
    public const int DefaultBombThreat = 3;
    public const int MinimumLives = 1;
    public const int DefaultLives = 3;

    private int _bombs;

    public int Columns { get; init; } = DefaultColumns;
    public int Rows { get; init; } = DefaultRows;
    public int Bombs { get => _bombs > 0 ? _bombs : Squares / DefaultBombThreat; init => _bombs = value; }
    public int Lives { get; init; } = DefaultLives;
    public int Squares => Columns * Rows;
}
