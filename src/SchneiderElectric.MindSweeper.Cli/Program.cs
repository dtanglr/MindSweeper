﻿using System.CommandLine.Hosting;
using System.Resources;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Application;
using SchneiderElectric.MindSweeper.Persistence;

[assembly: NeutralResourcesLanguage("en")]

namespace SchneiderElectric.MindSweeper.Cli;

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
