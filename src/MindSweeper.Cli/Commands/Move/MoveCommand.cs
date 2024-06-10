using MindSweeper.Application.Mediator.Commands.Move;

namespace MindSweeper.Cli.Commands.Move;

/// <summary>
/// Represents a command to move in the MindSweeper game.
/// </summary>
internal class MoveCommand : CliCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommand"/> class.
    /// </summary>
    public MoveCommand() : base("move", Resources.MoveCommandDescription)
    {
        // Add arguments
        Arguments.Add(new DirectionArgument());

        // Add action
        Action = CommandHandler.Create<Direction, IGameConsole, IMediator>(async (direction, console, mediator) =>
        {
            var request = new MoveCommandRequest(direction);
            var result = await mediator.Send(request);
            var view = new MoveCommandView(console);
            view.Render(request, result);
        });
    }
}
