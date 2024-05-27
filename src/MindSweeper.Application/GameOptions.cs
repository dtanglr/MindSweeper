using MindSweeper.Domain;

namespace MindSweeper.Application;

/// <summary>
/// Represents the options for the game.
/// </summary>
public class GameOptions
{
    /// <summary>
    /// Gets or sets the type of the game repository.
    /// </summary>
    public Type? RepositoryType { get; private set; }

    /// <summary>
    /// Sets the game repository to be used.
    /// </summary>
    /// <typeparam name="TRepository">The type of the game repository.</typeparam>
    /// <remarks>
    /// The repository type must implement the <see cref="IGameRepository"/> interface.
    /// </remarks>
    public void UseRepository<TRepository>() where TRepository : class, IGameRepository
    {
        RepositoryType = typeof(TRepository);
    }
}
