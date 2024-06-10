using MindSweeper.Application.Mediator.Commands.Start;

namespace MindSweeper.Cli.Commands.Start;

/// <summary>
/// Represents a command to start the game.
/// </summary>
internal class StartCommand : CliCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommand"/> class.
    /// </summary>
    public StartCommand() : base("start", Resources.StartCommandDescription)
    {
        // Add options
        Options.Add(new ColumnsOption());
        Options.Add(new RowsOption());
        Options.Add(new BombsOption());
        Options.Add(new LivesOption());

        // Add action
        Action = CommandHandler.Create<StartOptions, IGameConsole, IMediator>(async (options, console, mediator) =>
        {
            var request = new StartCommandRequest(options.Settings);
            var result = await mediator.Send(request);
            var view = new StartCommandView(console);
            view.Render(result);
        });
    }
}
