namespace MindSweeper.Domain;

/// <summary>
/// Represents the options for the game.
/// </summary>
public sealed class GameOptions
{
    /// <summary>
    /// Gets or sets the factory function that creates the player context.
    /// </summary>
    public Func<IServiceProvider, PlayerContext> PlayerContextFactory { get; set; } = _ => new PlayerContext(Environment.MachineName);
}
