namespace MindSweeper.Domain;

/// <summary>
/// The settings for the MindSweeper game.
/// </summary>
public sealed class GameSettings
{
    /// <summary>
    /// The column names used in the game.
    /// </summary>
    public const string ColumnNames = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// The minimum number of columns allowed.
    /// </summary>
    public const int MinimumColumns = 3;

    /// <summary>
    /// The maximum number of columns allowed.
    /// </summary>
    public const int MaximumColumns = 26;

    /// <summary>
    /// The default number of columns.
    /// </summary>
    public const int DefaultColumns = 8;

    /// <summary>
    /// The minimum number of rows allowed.
    /// </summary>
    public const int MinimumRows = 3;

    /// <summary>
    /// The maximum number of rows allowed.
    /// </summary>
    public const int MaximumRows = 26;

    /// <summary>
    /// The default number of rows.
    /// </summary>
    public const int DefaultRows = 8;

    /// <summary>
    /// The minimum number of bombs allowed.
    /// </summary>
    public const int MinimumBombs = 1;

    /// <summary>
    /// The default bomb threat level.
    /// </summary>
    public const int DefaultBombThreat = 3;

    /// <summary>
    /// The minimum number of lives allowed.
    /// </summary>
    public const int MinimumLives = 1;

    /// <summary>
    /// The default number of lives.
    /// </summary>
    public const int DefaultLives = 3;

    /// <summary>
    /// The number of bombs in the game.
    /// </summary>
    /// <remarks>
    /// If the value of <see cref="Bombs"/> is greater than <see cref="int.MinValue"/>, it represents the number of bombs set by the user.
    /// Otherwise, it is calculated based on the number of squares in the game and the default bomb threat level.
    /// </remarks>
    private int _bombs = int.MinValue;

    /// <summary>
    /// The number of columns in the game.
    /// </summary>
    public int Columns { get; init; } = DefaultColumns;

    /// <summary>
    /// The number of rows in the game.
    /// </summary>
    public int Rows { get; init; } = DefaultRows;

    /// <summary>
    /// The number of bombs in the game.
    /// </summary>
    /// <value>
    /// If the number of bombs is not set by the user, they are calculated based on the number of squares in the game divided by the default bomb threat level.
    /// </value>
    /// <remarks>
    /// The default bomb threat level is not currently configurable.
    /// </remarks>
    public int Bombs
    {
        get => _bombs > int.MinValue ? _bombs : Squares / DefaultBombThreat;
        init => _bombs = value;
    }

    /// <summary>
    /// The number of lives in the game.
    /// </summary>
    public int Lives { get; init; } = DefaultLives;

    /// <summary>
    /// The total number of squares in the game.
    /// </summary>
    public int Squares => Columns * Rows;
}
