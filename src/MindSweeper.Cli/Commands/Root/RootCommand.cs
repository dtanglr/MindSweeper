using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using MindSweeper.Cli.Commands.End;
using MindSweeper.Cli.Commands.Move;
using MindSweeper.Cli.Commands.Start;
using MindSweeper.Cli.Commands.Status;

namespace MindSweeper.Cli.Commands.Root;

/// <summary>
/// Represents the root command for the MindSweeper CLI.
/// </summary>
public class RootCommand : CliRootCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RootCommand"/> class.
    /// </summary>
    public RootCommand() : base(Resources.RootCommandDescription)
    {
        // Add sub-commands to the root command.
        // e.g. mindsweeper start, mindsweeper move, mindsweeper end, mindsweeper status
        Subcommands.Add(new StartCommand());
        Subcommands.Add(new MoveCommand());
        Subcommands.Add(new StatusCommand());
        Subcommands.Add(new EndCommand());

        // Add an action to the root command.
        // Displays the game logo and help text for the root command.
        Action = CommandHandler.Create<IConsole, ParseResult>((console, parseResult) =>
        {
            // Display the logo.
            console.Write(Resources.Logo);

            // Get and display the help text for the root command.
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
        });
    }

    /// <summary>
    /// Implicitly converts a <see cref="RootCommand"/> to a <see cref="CliConfiguration"/>.
    /// </summary>
    /// <param name="command">The <see cref="RootCommand"/> instance to convert.</param>
    /// <returns>A <see cref="CliConfiguration"/> instance.</returns>
    public static implicit operator CliConfiguration(RootCommand command) => new(command);
}
