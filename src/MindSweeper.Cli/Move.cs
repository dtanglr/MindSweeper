using MindSweeper.Application.Mediator.Commands.Move;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli;

partial class Program
{
    /// <summary>
    /// Represents the CLI command for moving in the game.
    /// </summary>
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
                        Direction[] completions =
                        [
                            Direction.Up,
                            Direction.Down,
                            Direction.Left,
                            Direction.Right
                        ];

                        return completions.Select(c => new CompletionItem(c.ToString().ToLower()));
                    }
                }
            }
        },
        Action = CommandHandler.Create<Direction, IHost>(async (direction, host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var command = new MoveCommand(direction);
            var result = await mediator.Send(command);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                    var response = result.Value!;
                    var game = response.Game;
                    var move = game.LastMove!;

                    Console.WriteLine(Resources.MoveCommandDetails,
                        move.Direction.ToString().ToLowerInvariant(),
                        move.FromSquare,
                        move.ToSquare);

                    Console.WriteLine();

                    switch (game.Status)
                    {
                        case GameStatus.InProgress:
                            Console.WriteLine(move.HitBomb ? Resources.Boom : Resources.Yes);
                            Console.WriteLine(move.HitBomb ? Resources.MoveCommandDidHitBomb : Resources.MoveCommandDidNotHitBomb);
                            Console.WriteLine();
                            Console.WriteLine(Resources.MoveCommandResultStatusAccepted);
                            break;
                        case GameStatus.Won:
                            Console.Write(Resources.GameOver);
                            Console.Write(Resources.YouWin);
                            break;
                        case GameStatus.Lost:
                            Console.Write(Resources.Boom);
                            Console.Write(Resources.GameOver);
                            Console.Write(Resources.YouLose);
                            break;
                        default:
                            break;
                    }

                    Console.WriteLine();
                    Console.WriteLine(game.GetGameStatus());
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
