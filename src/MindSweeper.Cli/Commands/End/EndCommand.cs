using MindSweeper.Domain.Results;
using Request = MindSweeper.Application.Mediator.Commands.End.EndCommand;

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
        Action = CommandHandler.Create<IGameConsole, IMediator>(async (console, mediator) =>
        {
            var command = new Request();
            var result = await mediator.Send(command);

            console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                    console.WriteLine(Resources.EndCommandResultStatusAccepted);
                    break;
                case ResultStatus.NotFound:
                    console.WriteLine(Resources.EndCommandResultStatusNotFound);
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
