using System.Runtime.InteropServices;
using SchneiderElectric.MindSweeper.Application.Commands.Move;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    public static CliCommand MoveCommand => new("move", Resources.MoveCommandDescription)
    {
        Arguments =
        {
            new CliArgument<Direction>("direction")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = Resources.MoveCommandDirectionArgumentDescription,
                HelpName = Resources.MoveCommandDirectionArgumentHelpName,
                CompletionSources =
                {
                    (context) =>
                    {
                        Direction[] completions = [ Direction.Up, Direction.Down, Direction.Left, Direction.Right ];
                        return completions.Select(c => new CompletionItem(c.ToString().ToLower()));
                    }
                }
            }
        },
        Action = CommandHandler.Create<Direction, IHost>(async (direction, host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var command = new MoveCommand(Environment.MachineName, direction);
            var result = await mediator.Send(command).ConfigureAwait(false);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                    var response = result.Value!;
                    var game = response.Game;

                    Console.WriteLine(Resources.MoveCommandDetails,
                        response.Direction.ToString().ToLowerInvariant(),
                        response.FromSquare,
                        response.ToSquare);

                    Console.WriteLine();

                    switch (game.Status)
                    {
                        case GameStatus.InProgress:
                            if (response.HitBomb)
                            {
                                Console.WriteLine(Resources.Boom);
                                Console.WriteLine(Resources.MoveCommandDidHitBomb);
                            }
                            else
                            {
                                Console.WriteLine(Resources.Yes);
                                Console.WriteLine(Resources.MoveCommandDidNotHitBomb);
                            }

                            Console.WriteLine();
                            Console.WriteLine(Resources.MoveCommandResultStatusAccepted);
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
                        case GameStatus.Won:
                            Console.Write(Resources.GameOver);
                            Console.Write(Resources.YouWin);
                            Console.WriteLine();
                            Console.WriteLine(Resources.GameStatusMoves, game.Moves);
                            Console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                            Console.WriteLine(Resources.GameStatusLives, game.Lives);
                            break;
                        case GameStatus.Lost:
                            Console.Write(Resources.GameOver);
                            Console.Write(Resources.YouLose);
                            Console.WriteLine();
                            Console.WriteLine(Resources.GameStatusMoves, game.Moves);
                            Console.WriteLine(Resources.GameStatusBombsHit, game.BombsHit);
                            break;
                        default:
                            break;
                    }

                    break;
                case ResultStatus.Unprocessable:
                    Console.WriteLine(Resources.MoveCommandResultStatusUnprocessable, direction);
                    break;
                case ResultStatus.NotFound:
                    Console.WriteLine(Resources.MoveCommandResultStatusNotFound);
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
