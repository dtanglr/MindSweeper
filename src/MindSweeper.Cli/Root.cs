using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;

namespace MindSweeper.Cli;

partial class Program
{
    /// <summary>
    /// Gets the root command for the CLI application.
    /// </summary>
    public static CliRootCommand RootCommand => new(Resources.RootCommandDescription)
    {
        Subcommands = { StartCommand, MoveCommand, EndCommand, StatusCommand },
        Action = CommandHandler.Create<IHost>((host) =>
        {
            Console.Write(Resources.Logo);

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
