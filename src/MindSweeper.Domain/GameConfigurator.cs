using Microsoft.Extensions.DependencyInjection;

namespace MindSweeper.Domain;

/// <summary>
/// Represents a game configurator.
/// </summary>
public sealed class GameConfigurator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameConfigurator"/> class.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="options">The game options.</param>
    public GameConfigurator(IServiceCollection services, GameOptions options) =>
        (Services, Options) = (services, options);

    /// <summary>
    /// Gets the service collection.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Gets the game options.
    /// </summary>
    public GameOptions Options { get; }
}
