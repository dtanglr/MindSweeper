using System.CommandLine.Help;
using System.CommandLine.Invocation;

namespace MindSweeper.Cli.Commands.Root;

/// <summary>
/// Represents the root view of the MindSweeper CLI.
/// </summary>
internal class RootCommandView : ICommandView<ParseResult>
{
    private readonly IGameConsole _console;

    /// <summary>
    /// Initializes a new instance of the <see cref="RootCommandView"/> class.
    /// </summary>
    /// <param name="console">The game console used for rendering.</param>
    public RootCommandView(IGameConsole console)
    {
        _console = console;
    }

    /// <summary>
    /// Renders the view for the parse result.
    /// </summary>
    /// <param name="result">The parse result.</param>
    public void Render(ParseResult result)
    {
        // Display the logo.
        _console.Write(Resources.Logo);

        // Get and display the help text for the root command.
        var availableHelpOptions = result.RootCommandResult.Command.Options.OfType<HelpOption>();

        if (availableHelpOptions.FirstOrDefault(o => o is not null) is { Action: not null } helpOption)
        {
            switch (helpOption.Action)
            {
                case SynchronousCliAction syncAction:
                    syncAction.Invoke(result);
                    break;

                case AsynchronousCliAction asyncAction:
                    asyncAction.InvokeAsync(result, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
                    break;
            }
        }
    }
}
