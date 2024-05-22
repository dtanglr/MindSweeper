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
    public static CliRootCommand RootCommand => new(
        "Once started, you will be placed on a square on the very bottom of a playing field that's similar to a chess board. " +
        "To compete you must issue simple CLI commands to move one square at a time in any direction of your choosing. " +
        "Each move requires a great deal of consideration because the destination square may or may not have been " +
        "randomly allocated an explosive device. Stepping on such as square will kill you! " +
        "To win, you must make it to the top without losing all your lives. Good luck.")
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
            Console.Write(Ascii.Logo);

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
