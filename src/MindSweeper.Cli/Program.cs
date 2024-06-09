using System.CommandLine.Hosting;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Mediator;
using MindSweeper.Cli.Commands.Root;
using MindSweeper.Persistence.LocalFile;

namespace MindSweeper.Cli;

/// <summary>
/// Entry point of the application.
/// </summary>
/// <param name="args">Command-line arguments.</param>
/// <returns>Exit code of the application.</returns>
internal class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <returns>Exit code of the application.</returns>
    static Task<int> Main(string[] args) => BuildMindSweeper()
        .UseHost(_ => Host.CreateDefaultBuilder(),
            host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddSingleton<IGameConsole, GameConsole>();
                    services.ConfigureMindSweeper(configure =>
                    {
                        configure.PlayerContextFactory = _ => new PlayerContext(Environment.MachineName);
                    })
                    .UseMediatorPipeline()
                    .UseLocalFileStorage(configure =>
                    {
                        configure.JsonSerializerOptions = options => options.WriteIndented = true;
                    });
                });

                host.ConfigureLogging(logging =>
                    logging.SetMinimumLevel(LogLevel.Error));
            })
        .InvokeAsync(args);

    /// <summary>
    /// Builds the MindSweeper CLI application.
    /// </summary>
    /// <returns>The CLI configuration.</returns>
    /// <remarks>The <see cref="RootCommand"/> class implicitly converts to an instance of a <see cref="CliConfiguration"/> class</remarks>
    static CliConfiguration BuildMindSweeper() => new RootCommand();
}
