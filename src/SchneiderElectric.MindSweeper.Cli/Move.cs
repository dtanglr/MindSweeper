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
                        case GameStatus.Won:
                            Console.WriteLine("Game over, you win!");
                            Console.WriteLine($"Total moves: {response.Game.Moves}");
                            break;
                        case GameStatus.Lost:
                            Console.WriteLine("Game over, you lose!");
                            Console.WriteLine($"Total moves: {response.Game.Moves}");
                            break;
                        default:
                            Console.WriteLine($"You are currently on square: {response.Game.CurrentSquare}");
                            Console.WriteLine($"Based on your current square, you can move: {string.Join(", ", response.Game.AvailableMoves.Select(m => m.Key.ToString()))}");
                            Console.WriteLine($"You have made {response.Game.Moves} moves so far and have {response.Game.Lives} {(response.Game.Lives > 1 ? "lives" : "life")} left in this game.");
                            break;
                    }

                    break;
                case ResultStatus.Unprocessable:
                    Console.WriteLine($"It's not possible to move '{direction}' from your current square.");
                    break;
                case ResultStatus.NotFound:
                    Console.WriteLine("There is no game to make a move on!. Start a new game first!");
                    break;
                case ResultStatus.Error:
                    Console.WriteLine("Unfortunately an error occurred.");
                    result.Errors.ForEach(e => Console.WriteLine(e));
                    break;
                default:
                    Console.WriteLine($"Unexpected result: {result.Status}");
                    break;
            }
        })
    };
}
