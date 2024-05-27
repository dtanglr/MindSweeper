namespace MindSweeper.Domain;

/// <summary>
/// Represents the status of a game.
/// </summary>
public enum GameStatus
{
    /// <summary>
    /// The game is in progress.
    /// </summary>
    InProgress,

    /// <summary>
    /// The game has been won.
    /// </summary>
    Won,

    /// <summary>
    /// The game has been lost.
    /// </summary>
    Lost
}
