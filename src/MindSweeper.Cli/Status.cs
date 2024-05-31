using MindSweeper.Application.Requests.GetGame;

namespace MindSweeper.Cli;

partial class Program
{
    /// <summary>
    /// Represents the CLI command for getting the current game status.
    /// </summary>
    public static CliCommand StatusCommand => new("status", Resources.StatusCommandDescription)
    {
        Action = CommandHandler.Create<IHost>(async (host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var request = new GetGameRequest();
            var result = await mediator.Send(request);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Ok:
                    var game = result.Value!.Game;
                    Console.WriteLine(Resources.StatusCommandResultStatusOk);
                    Console.WriteLine();
                    Console.WriteLine(game.GetGameStatus());
                    break;
                case ResultStatus.NotFound:
                    Console.WriteLine(Resources.StatusCommandResultStatusNotFound);
                    break;
                case ResultStatus.Invalid:
                    Console.WriteLine(Resources.CommandResultStatusInvalid);
                    result.ValidationIssues.ForEach(e => Console.WriteLine(e.Message));
                    break;
                case ResultStatus.Error:
                    Console.WriteLine(Resources.CommandResultStatusError);
                    result.Errors.ForEach(Console.WriteLine);
                    break;
                default:
                    Console.WriteLine(Resources.CommandResultStatusUnhandled, result.Status);
                    break;
            }

            Console.WriteLine();
        })
    };
}
