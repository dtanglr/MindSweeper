using MindSweeper.Domain.Results;

namespace MindSweeper.Domain;

/// <summary>
/// Represents a repository for managing game entities.
/// </summary>
public interface IGameRepository
{
    /// <summary>
    /// Creates a new game asynchronously.
    /// </summary>
    /// <param name="newGame">The new game to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with a Result indicating the success or failure of the operation.</returns>
    Task<Result> CreateGameAsync(Game newGame, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a game asynchronously.
    /// </summary>
    /// <param name="gameId">The ID of the game.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with a Result indicating the success or failure of the operation.</returns>
    Task<Result> DeleteGameAsync(Guid gameId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a game asynchronously.
    /// </summary>
    /// <param name="playerId">The ID of the player.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with a Result containing the retrieved game or indicating the failure of the operation.</returns>
    Task<Result<Game>> GetGameAsync(string playerId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a game asynchronously.
    /// </summary>
    /// <param name="updatedGame">The updated game.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation with a Result indicating the success or failure of the operation.</returns>
    Task<Result> UpdateGameAsync(Game updatedGame, CancellationToken cancellationToken);
}
