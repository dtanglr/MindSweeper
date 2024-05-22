using System.CommandLine;
using System.CommandLine.Completions;
using System.CommandLine.NamingConventionBinder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchneiderElectric.MindSweeper.Application.Commands.Move;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    public static CliCommand MoveCommand => new("move", "Move to a square")
    {
        Arguments =
        {
            new CliArgument<Direction>("direction")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = "Choices can be either 'up', 'down', 'left' or 'right' but are dependent on your current square.",
                HelpName = "direction",
                CompletionSources =
                {
                    (context) =>
                    {
                        var completions = new List<string> { "up", "down", "left", "right" };
                        return completions.Select(c => new CompletionItem(c));
                    }
                }
            }
        },
        Action = CommandHandler.Create<Direction, IHost>(async (direction, host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var command = new MoveCommand(Environment.MachineName, direction);
            var result = await mediator.Send(command);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                case ResultStatus.Ok:
                    var response = result.Value!;
                    Console.WriteLine($"Moving '{response.Direction}' from '{response.FromSquare}' to '{response.ToSquare}'...");

                    var explosiveOutcome = response.HitBomb ? "Ooops. You hit a bomb!" : "Yay! You didn't hit a bomb.";
                    Console.WriteLine(explosiveOutcome);

                    switch (response.Game.Status)
                    {
                        case GameStatus.InProgress:
                            Console.WriteLine($"You are currently on square: {response.Game.CurrentSquare}");
                            Console.WriteLine($"Based on your current square, you can move: {string.Join(", ", response.Game.AvailableMoves.Select(m => m.Key.ToString()))}");
                            Console.WriteLine($"You have made {response.Game.Moves} moves so far and have {response.Game.Lives} {(response.Game.Lives > 1 ? "lives" : "life")} left in this game.");
                            break;
                        case GameStatus.Won:
                            Console.Write(Ascii.GameOver);
                            Console.Write(Ascii.YouWin);
                            Console.WriteLine();
                            Console.WriteLine($"You won in {response.Game.Moves} moves and had {response.Game.Lives} {(response.Game.Lives > 1 ? "lives" : "life")} left.");
                            break;
                        case GameStatus.Lost:
                            Console.Write(Ascii.GameOver);
                            Console.Write(Ascii.YouLose);
                            Console.WriteLine();
                            Console.WriteLine($"You lost in {response.Game.Moves} moves.");
                            break;
                        default:
                            break;
                    }

                    break;
                case ResultStatus.Unprocessable:
                    Console.WriteLine($"It's not possible to move '{direction}' from your current square.");
                    break;
                case ResultStatus.NotFound:
                    Console.WriteLine("There is no game to make a move on!. Start a new game first!");
                    break;
                case ResultStatus.Invalid:
                    Console.WriteLine("One or more validation issues occurred.");
                    result.ValidationIssues.ForEach(e => Console.WriteLine(e.Message));
                    break;
                case ResultStatus.Error:
                    Console.WriteLine("Unfortunately an error occurred.");
                    result.Errors.ForEach(Console.WriteLine);
                    break;
                default:
                    Console.WriteLine($"Unexpected result: {result.Status}");
                    break;
            }

            Console.WriteLine();
        })
    };
}
