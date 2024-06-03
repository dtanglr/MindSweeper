namespace MindSweeper.Domain;

/// <summary>
/// Represents a MindSweeper game instance for a player.
/// </summary>
public record Game
{
    /// <summary>
    /// Gets the game ID.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Gets the player ID.
    /// </summary>
    public required string PlayerId { get; init; }

    /// <summary>
    /// Gets the game settings.
    /// </summary>
    public required GameSettings Settings { get; init; }

    /// <summary>
    /// Gets the collection of bomb positions.
    /// </summary>
    public required List<int> Bombs { get; init; }

    /// <summary>
    /// Gets or sets the current square.
    /// </summary>
    public required string CurrentSquare { get; set; }

    /// <summary>
    /// Gets or sets the available moves.
    /// </summary>
    public required Dictionary<Direction, string> AvailableMoves { get; set; }

    /// <summary>
    /// Gets the number of bombs hit in the game.
    /// </summary>
    public int BombsHit { get; set; }

    /// <summary>
    /// Gets or sets the number of lives remaining in the game.
    /// </summary>
    public int Lives { get; set; }

    /// <summary>
    /// Gets or sets the number of moves made in the game.
    /// </summary>
    public int Moves { get; set; }

    /// <summary>
    /// Gets the last move made in the game.
    /// </summary>
    public Move? LastMove { get; set; }

    /// <summary>
    /// Gets or sets the moves made in the game.
    /// </summary>
    public List<Move> MovesMade { get; set; } = [];

    /// <summary>
    /// Gets or sets the status of the game.
    /// </summary>
    public GameStatus Status { get; set; } = GameStatus.InProgress;

    /// <summary>
    /// Creates a deep copy of the game instance.
    /// </summary>
    /// <returns>A deep copy of the game instance.</returns>
    public Game DeepCopy()
    {
        var game = (Game)MemberwiseClone();
        game.MovesMade = new List<Move>(MovesMade);

        return game;
    }
}
