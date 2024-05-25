using System.CommandLine.Hosting;
using Microsoft.Extensions.Logging;
using MindSweeper.Application;
using MindSweeper.Persistence;

namespace MindSweeper.Cli;

partial class Program : IProgram
{
    public static Task<int> Main(string[] args) => new CliConfiguration(RootCommand)
        .UseHost(_ => Host.CreateDefaultBuilder(),
            host =>
            {
                host.ConfigureServices(services =>
                    services.AddMindGame(options =>
                        options.UseRepository<JsonFileGameRepository>()));

                host.ConfigureLogging(logging =>
                    logging.SetMinimumLevel(LogLevel.Error));
            })
        .InvokeAsync(args);
}
