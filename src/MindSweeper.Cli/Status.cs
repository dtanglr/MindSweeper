using MindSweeper.Application.Mediator.Queries.GetGame;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli;

partial class Program
{
    /// <summary>
    /// Represents the CLI command for getting the current game status.
    /// </summary>
    public static CliCommand StatusCommand => new("status", Resources.StatusCommandDescription)
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
                    console.WriteGameStatus(game);
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
        })
    };
}
