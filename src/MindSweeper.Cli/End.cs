using MindSweeper.Application.Mediator.Commands.End;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli;

partial class Program
{
    /// <summary>
    /// Represents the CLI command for ending the game.
    /// </summary>
    public static CliCommand EndCommand => new("end", Resources.EndCommandDescription)
    {
        Action = CommandHandler.Create<IConsole, IMediator>(async (console, mediator) =>
        {
            var command = new EndCommand();
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
        })
    };
}
