using System.CommandLine;
using System.CommandLine.Completions;
using System.CommandLine.Hosting;
using System.CommandLine.NamingConventionBinder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Application;

namespace SchneiderElectric.MindSweeper.Cli;

public static class Program
{
    private static Task<int> Main(string[] args) => BuildCommandLine()
        .UseHost(_ => Host.CreateDefaultBuilder(),
            host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddMindGame();
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
                new CliOption<string>("--name", "-n")
                {
                    Arity = ArgumentArity.ExactlyOne,
                    Description = "The name of the player",
                    HelpName = "name",
                    Required = false,
                    DefaultValueFactory = (arg) => "Manic Miner"
                },
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

        // Root command
        var rootCommand = new CliRootCommand("A game to sweep the mind of it's creator by it's players :-P")
        {
            Action = CommandHandler.Create<IHost>(Root),
            Subcommands = { startCommand, moveCommand }
        };

        return new CliConfiguration(rootCommand);
    }

    private static void Root(IHost host)
    {
        var logger = host.CreateLogger();
        logger.LogInformation("Root command handled");
    }

    private static void Start(StartOptions options, IHost host)
    {
        var logger = host.CreateLogger();
        logger.LogInformation("Start command handled with options - Name: {Name}, Columns: {Columns}, Rows: {Rows}, Bombs: {Bombs}, Lives: {Lives}",
            options.Name, options.Columns, options.Rows, options.Bombs, options.Lives);
    }

    private static void Move(Direction direction, IHost host)
    {
        var logger = host.CreateLogger();
        logger.LogInformation("Move {Direction} command handled", direction);
    }

    private static ILogger CreateLogger(this IHost host)
    {
        var serviceProvider = host.Services;
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        return loggerFactory.CreateLogger(typeof(Program));
    }
}
