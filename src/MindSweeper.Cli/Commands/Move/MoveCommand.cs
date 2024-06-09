using MindSweeper.Domain.Results;
using Request = MindSweeper.Application.Mediator.Commands.Move.MoveCommand;

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
        Arguments.Add(new DirectionArgument());
        Action = CommandHandler.Create<Direction, IGameConsole, IMediator>(async (direction, console, mediator) =>
        {
            var command = new Request(direction);
            var result = await mediator.Send(command);

            console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                    var response = result.Value!;
                    var game = response.Game;
                    var move = game.LastMove!;

                    console.WriteLine(Resources.MoveCommandDetails,
                        move.Direction.ToString().ToLowerInvariant(),
                        move.FromSquare,
                        move.ToSquare);

                    console.WriteLine();

                    switch (game.Status)
                    {
                        case GameStatus.InProgress:
                            console.WriteLine(move.HitBomb ? Resources.Boom : Resources.Yes);
                            console.WriteLine(move.HitBomb ? Resources.MoveCommandDidHitBomb : Resources.MoveCommandDidNotHitBomb);
                            console.WriteLine();
                            console.WriteLine(Resources.MoveCommandResultStatusAccepted);
                            break;
                        case GameStatus.Won:
                            console.Write(Resources.GameOver);
                            console.Write(Resources.YouWin);
                            break;
                        case GameStatus.Lost:
                            console.Write(Resources.Boom);
                            console.Write(Resources.GameOver);
                            console.Write(Resources.YouLose);
                            break;
                        default:
                            break;
                    }

                    console.WriteLine();
                    console.Write(game);
                    break;
                case ResultStatus.Unprocessable:
                    console.WriteLine(Resources.MoveCommandResultStatusUnprocessable, direction);
                    break;
                case ResultStatus.NotFound:
                    console.WriteLine(Resources.MoveCommandResultStatusNotFound);
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
