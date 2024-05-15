﻿using System.CommandLine;
using System.CommandLine.Hosting;
using Microsoft.Extensions.Hosting;
using SchneiderElectric.MindSweeper.Application;
using SchneiderElectric.MindSweeper.Persistence;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program : IProgram
{
    public static Task<int> Main(string[] args) => new CliConfiguration(RootCommand)
        .UseHost(_ => Host.CreateDefaultBuilder(),
            host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddMindGame(options =>
                        options.UseRepository<JsonFileGameRepository>());
                });
            })
        .InvokeAsync(args);
}
