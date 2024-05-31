namespace MindSweeper.Domain;

/// <summary>
/// Represents the context of a player.
/// </summary>
public record PlayerContext(string Id)
{
    /// <summary>
    /// Gets or sets the game associated with the player.
    /// </summary>
    public Game? Game { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether the player has a game.
    /// </summary>
    public bool HasGame => Game is not null;
}
