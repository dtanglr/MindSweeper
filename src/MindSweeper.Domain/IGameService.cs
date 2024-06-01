using MindSweeper.Domain.Results;

namespace MindSweeper.Domain;

/// <summary>
/// Represents a game service interface.
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Starts a new game with the specified settings asynchronously.
    /// </summary>
    /// <param name="settings">The game settings.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<Game>> StartAsync(GameSettings settings, CancellationToken cancellationToken);

    /// <summary>
    /// Moves the game in the specified direction asynchronously.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<Game>> MoveAsync(Direction direction, CancellationToken cancellationToken);

    /// <summary>
    /// Ends the game asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result> EndAsync(CancellationToken cancellationToken);
}
