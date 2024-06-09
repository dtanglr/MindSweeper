using MindSweeper.Application.Mediator.Queries.GetGame;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.Commands.Status;

/// <summary>
/// Represents a command that displays the status of the game.
/// </summary>
public class StatusCommand : CliCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StatusCommand"/> class.
    /// </summary>
    public StatusCommand() : base("status", Resources.StatusCommandDescription)
    {
        Action = CommandHandler.Create<IConsole, IMediator>(async (console, mediator) =>
        {
            var query = new GetGameQuery();
            var result = await mediator.Send(query);

            console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Ok:
                    var game = result.Value!.Game;
                    console.WriteLine(Resources.StatusCommandResultStatusOk);
                    console.WriteLine();
                    console.Write(game);
                    break;
                case ResultStatus.NotFound:
                    console.WriteLine(Resources.StatusCommandResultStatusNotFound);
                    break;
                case ResultStatus.Invalid:
                    console.WriteLine(Resources.CommandResultStatusInvalid);
                    result.ValidationIssues.ForEach(e => console.WriteLine(e.Message));
                    break;
                case ResultStatus.Error:
                    console.WriteLine(Resources.CommandResultStatusError);
                    result.Errors.ForEach(console.WriteLine);
                    break;
                default:
                    console.WriteLine(Resources.CommandResultStatusUnhandled, result.Status);
                    break;
            }

            console.WriteLine();
        });
    }
}
