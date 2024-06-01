using System.CommandLine.Hosting;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Mediator;
using MindSweeper.Persistence.LocalFile;

namespace MindSweeper.Cli;

partial class Program : IProgram
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <returns>Exit code of the application.</returns>
    public static Task<int> Main(string[] args) => new CliConfiguration(RootCommand)
        .UseHost(_ => Host.CreateDefaultBuilder(),
            host =>
            {
                host.ConfigureServices(services =>
                {
                    services.ConfigureMindSweeper(configure =>
                    {
                        configure.PlayerContextFactory = _ => new PlayerContext(Environment.MachineName);
                    })
                    .UseMediatorPipeline()
                    .UseLocalFileStorage(configure =>
                    {
                        configure.JsonSerializerOptions = options =>
                        {
                            options.WriteIndented = true;
                        };
                    });
                });

                host.ConfigureLogging(logging =>
                    logging.SetMinimumLevel(LogLevel.Error));
            })
        .InvokeAsync(args);
}
