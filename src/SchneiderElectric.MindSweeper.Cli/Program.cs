using System.CommandLine;
using System.CommandLine.Completions;
using System.CommandLine.Hosting;
using System.CommandLine.NamingConventionBinder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Application;
using SchneiderElectric.MindSweeper.Domain;
using SchneiderElectric.MindSweeper.Persistence;

namespace SchneiderElectric.MindSweeper.Cli;

static partial class Program
{
    private static Task<int> Main(string[] args) => BuildCommandLine()
        .UseHost(_ => Host.CreateDefaultBuilder(),
            host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddMindGame(options => options.UseRepository<JsonFileGameRepository>());
                });
            })
        .InvokeAsync(args);

    private static CliConfiguration BuildCommandLine()
    {
        // Start command
        var startCommand = new CliCommand("start", "Start a new game")
        {
            Action = CommandHandler.Create<StartOptions, IHost>(Start),
            Options =
            {
                new CliOption<int>("--columns", "-c", "-cols")
                {
                    Arity = ArgumentArity.ExactlyOne,
                    Description = "The number of columns",
                    HelpName = "columns",
                    Required = false,
                    DefaultValueFactory = (arg) => Settings.DefaultColumns
                },
                new CliOption<int>("--rows", "-r")
                {
                    Arity = ArgumentArity.ExactlyOne,
                    Description = "The number of rows",
                    HelpName = "rows",
                    Required = false,
                    DefaultValueFactory = (arg) => Settings.DefaultRows
                },
                new CliOption<int>("--bombs", "-b")
                {
                    Arity = ArgumentArity.ExactlyOne,
                    Description = "The number of bombs",
                    HelpName = "bombs",
                    Required = false,
                    DefaultValueFactory = (arg) => Settings.DefaultBombs
                },
                new CliOption<int>("--lives", "-l")
                {
                    Arity = ArgumentArity.ExactlyOne,
                    Description = "The number of lives",
                    HelpName = "lives",
                    Required = false,
                    DefaultValueFactory = (arg) => Settings.DefaultLives
                }
            }
        };

        // Move command
        var directionArgument = new CliArgument<Direction>("direction")
        {
            Arity = ArgumentArity.ExactlyOne,
            Description = "Choices can be either 'up', 'down', 'left' or 'right' but are dependent on your current square.",
            HelpName = "direction",
            CompletionSources =
            {
                (context) =>
                {
                    // TODO: These choices can be dynamic based on the current square.
                    var completions = new List<string> { "up", "down", "left", "right" };
                    return completions.Select(c => new CompletionItem(c));
                }
            }
        };

        var moveCommand = new CliCommand("move", "Move to a square")
        {
            Action = CommandHandler.Create<Direction, IHost>(Move),
            Arguments = { directionArgument }
        };

        // End command
        var endCommand = new CliCommand("end", "End the current game")
        {
            Action = CommandHandler.Create<IHost>(End)
        };

        // Root command
        var rootCommand = new CliRootCommand("A game to sweep the mind of it's creator by it's players :-P")
        {
            Action = CommandHandler.Create<IHost>(Root),
            Subcommands = { startCommand, moveCommand, endCommand }
        };

        return new CliConfiguration(rootCommand);
    }

    private static ILogger CreateLogger(this IHost host)
    {
        var serviceProvider = host.Services;
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        return loggerFactory.CreateLogger(typeof(Program));
    }

    private static IMediator CreateMediator(this IHost host)
    {
        var serviceProvider = host.Services;
        return serviceProvider.GetRequiredService<IMediator>();
    }
}
