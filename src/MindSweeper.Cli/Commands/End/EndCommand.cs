using MindSweeper.Application.Mediator.Commands.End;

namespace MindSweeper.Cli.Commands.End;

/// <summary>
/// Represents the command to end the game.
/// </summary>
internal class EndCommand : CliCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EndCommand"/> class.
    /// </summary>
    public EndCommand() : base("end", Resources.EndCommandDescription)
    {
        // Add action
        Action = CommandHandler.Create<IGameConsole, IMediator>(async (console, mediator) =>
        {
            var request = new EndCommandRequest();
            var result = await mediator.Send(request);
            var view = new EndCommandView(console);
            view.Render(result);
        });
    }
}
