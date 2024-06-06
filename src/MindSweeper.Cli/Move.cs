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
                        var completions = Enum.GetValues<Direction>();

                        return completions.Select(c => new CompletionItem(c.ToString().ToLower()));
                    }
                }
            }
        },
        Action = CommandHandler.Create<Direction, IConsole, IMediator>(async (direction, console, mediator) =>
        {
            var command = new MoveCommand(direction);
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
                    console.WriteGameStatus(game);
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
        })
    };
}
