namespace MindSweeper.Cli;

/// <summary>
/// Provides dependency injection configuration for the MindSweeper CLI.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures the game configurator to use local file storage.
    /// </summary>
    /// <param name="configurator">The game configurator.</param>
    /// <param name="configure">The action to configure the JSON file options.</param>
    /// <returns>The game configurator.</returns>
    public static GameConfigurator UseCommandLine(this GameConfigurator configurator)
    {
        configurator.Services.AddScoped(typeof(IGameConsole), typeof(GameConsole));

        return configurator;
    }
}
