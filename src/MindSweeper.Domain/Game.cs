namespace MindSweeper.Domain;

/// <summary>
/// Represents a MindSweeper game instance for a player.
/// </summary>
public record Game(
    Guid Id,
    string PlayerId,
    Settings Settings,
    List<int> Bombs,
    int Lives,
    int Moves,
    string CurrentSquare,
    Dictionary<Direction, string> AvailableMoves)
{
    /// <summary>
    /// Gets the status of the game.
    /// </summary>
    public GameStatus Status => this switch
    {
        { Lives: 0 } => GameStatus.Lost,
        _ when !AvailableMoves.ContainsKey(Direction.Up) => GameStatus.Won,
        _ => GameStatus.InProgress
    };

    /// <summary>
    /// Gets the number of bombs hit in the game.
    /// </summary>
    public int BombsHit => this switch
    {
        { Lives: 0 } => Settings.Lives,
        _ when Lives == Settings.Lives => 0,
        _ when Lives < Settings.Lives => Settings.Lives - Lives,
        _ => throw new ArgumentOutOfRangeException(nameof(Lives))
    };
}
