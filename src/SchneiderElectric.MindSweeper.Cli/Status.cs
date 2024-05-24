using SchneiderElectric.MindSweeper.Application.Requests.GetGame;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    public static CliCommand StatusCommand => new("status", Resources.StatusCommandDescription)
    {
        Action = CommandHandler.Create<IHost>(async (host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var request = new GetGameRequest(Environment.MachineName);
            var result = await mediator.Send(request).ConfigureAwait(false);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Ok:
                    var game = result.Value!.Game;
                    Console.WriteLine(Resources.StatusCommandResultStatusOk);
                    Console.WriteLine();
                    Console.WriteLine(Resources.GameStatusRows, game.Settings.Rows);
                    Console.WriteLine(Resources.GameStatusColumns, game.Settings.Columns);
                    Console.WriteLine(Resources.GameStatusSquares, game.Settings.Squares);
                    Console.WriteLine(Resources.GameStatusBombs, game.Settings.Bombs);
                    Console.WriteLine(Resources.GameStatusCurrentSquare, game.CurrentSquare);
                    Console.WriteLine(Resources.GameStatusAvailableMoves, string.Join(", ", game.AvailableMoves.Select(m => $"{Environment.NewLine}    {m.Key} to {m.Value}")));
                    Console.WriteLine(Resources.GameStatusMoves, game.Moves);
                    Console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                    Console.WriteLine(Resources.GameStatusLives, game.Lives);
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
