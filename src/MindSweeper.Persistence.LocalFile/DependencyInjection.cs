using System.IO.Abstractions;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using MindSweeper.Domain;

namespace MindSweeper.Persistence.LocalFile;

/// <summary>
/// Provides dependency injection extension methods for using local file storage in the MindSweeper game.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures the game configurator to use local file storage.
    /// </summary>
    /// <param name="configurator">The game configurator.</param>
    /// <param name="configure">The action to configure the JSON file options.</param>
    /// <returns>The game configurator.</returns>
    public static GameConfigurator UseLocalFileStorage(this GameConfigurator configurator, Action<JsonFileOptions>? configure = null)
    {
        var options = new JsonFileOptions();
        configure?.Invoke(options);

        var jsonSerializerOptions = new JsonSerializerOptions();
        options.JsonSerializerOptions?.Invoke(jsonSerializerOptions);

        configurator.Services.AddScoped(typeof(IFileSystem), typeof(FileSystem));
        configurator.Services.AddScoped(typeof(JsonSerializerOptions), (_) => jsonSerializerOptions);
        configurator.Services.AddScoped(typeof(IGameRepository), typeof(JsonFileGameRepository));

        return configurator;
    }
}
