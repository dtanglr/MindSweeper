using MindSweeper.Cli.Commands.End;
using MindSweeper.Cli.Commands.Move;
using MindSweeper.Cli.Commands.Start;
using MindSweeper.Cli.Commands.Status;
using MindSweeper.Cli.Views;

namespace MindSweeper.Cli.Commands.Root;

/// <summary>
/// Represents the root command for the MindSweeper CLI.
/// </summary>
internal class RootCommand : CliRootCommand
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
        Action = CommandHandler.Create<IGameConsole, ParseResult>((console, parseResult) =>
        {
            var view = new RootCommandView(console);
            view.Render(parseResult);
        });
    }

    /// <summary>
    /// Implicitly converts a <see cref="RootCommand"/> to a <see cref="CliConfiguration"/>.
    /// </summary>
    /// <param name="command">The <see cref="RootCommand"/> instance to convert.</param>
    /// <returns>A <see cref="CliConfiguration"/> instance.</returns>
    public static implicit operator CliConfiguration(RootCommand command) => new(command);
}
