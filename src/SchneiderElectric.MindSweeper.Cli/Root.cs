using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine.Parsing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    private static readonly List<string> _gameHeader =
    [
        "___  ____           _   _____                      ",
        "|  \\/  (_)         | | |  __ \\                     ",
        "| .  . |_ _ __   __| | | |  \\/ __ _ _ __ ___   ___ ",
        "| |\\/| | | '_ \\ / _` | | | __ / _` | '_ ` _ \\ / _ \\",
        "| |  | | | | | | (_| | | |_\\ \\ (_| | | | | | |  __/",
        "\\_|  |_/_|_| |_|\\__,_|  \\____/\\__,_|_| |_| |_|\\___|",
        "                                  CLI Delux Edition",
        "                                                   "
    ];

    public static CliRootCommand RootCommand => new(
        "On a playing field, that's similar to a chess board, you can issue simple CLI commands to play the game by moving " +
        "from a randomly allocated square on the bottom row to the top row. You are allowed to move one square at a time. " +
        "Each move requires a great deal of consideration and skill because the destination square may or may not have been " +
        "randomly allocated an explosive device. If it does, it will kill you. If you die too many times, you will lose. " +
        "However, if you make it to the top without losing all your lives, you will be a winner and become god-like genius!. Good luck.")
    {
        Subcommands =
        {
            StartCommand,
            MoveCommand,
            EndCommand,
            StatusCommand
        },
        Action = CommandHandler.Create<IHost>((host) =>
        {
            _gameHeader.ForEach(Console.WriteLine);

            var parseResult = host.Services.GetRequiredService<ParseResult>();
            var availableHelpOptions = parseResult
                .CommandResult
                .RecurseWhileNotNull(r => r.Parent as CommandResult)
                .Select(r => r.Command.Options.OfType<HelpOption>().FirstOrDefault());

            if (availableHelpOptions.FirstOrDefault(o => o is not null) is { Action: not null } helpOption)
            {
                switch (helpOption.Action)
                {
                    case SynchronousCliAction syncAction:
                        syncAction.Invoke(parseResult);
                        break;

                    case AsynchronousCliAction asyncAction:
                        asyncAction.InvokeAsync(parseResult, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
                        break;
                }
            }
        })
    };
}
