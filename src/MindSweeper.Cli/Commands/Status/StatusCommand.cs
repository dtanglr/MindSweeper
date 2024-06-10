using MindSweeper.Application.Mediator.Queries.GetGame;

namespace MindSweeper.Cli.Commands.Status;

/// <summary>
/// Represents a command that displays the status of the game.
/// </summary>
internal class StatusCommand : CliCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StatusCommand"/> class.
    /// </summary>
    public StatusCommand() : base("status", Resources.StatusCommandDescription)
    {
        // Add action
        Action = CommandHandler.Create<IGameConsole, IMediator>(async (console, mediator) =>
        {
            var request = new GetGameQueryRequest();
            var result = await mediator.Send(request);
            var view = new StatusCommandView(console);
            view.Render(result);
        });
    }
}
