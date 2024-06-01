using Microsoft.Extensions.DependencyInjection;

namespace MindSweeper.Domain;

/// <summary>
/// Provides dependency injection configuration for the MindSweeper game.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the MindSweeper game services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configure">An action to configure the game options.</param>
    /// <returns>A <see cref="GameConfigurator"/> instance.</returns>
    public static GameConfigurator ConfigureMindSweeper(this IServiceCollection services, Action<GameOptions>? configure = null)
    {
        var options = new GameOptions();
        configure?.Invoke(options);

        services.AddScoped(typeof(PlayerContext), options.PlayerContextFactory);
        services.AddScoped<IGameService, GameService>();

        return new GameConfigurator(services, options);
    }
}
