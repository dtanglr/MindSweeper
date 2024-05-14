using Microsoft.Extensions.Hosting;
using SchneiderElectric.MindSweeper.Application.Commands.Move;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    private static async Task Move(Direction direction, IHost host)
    {
        var mediator = host.CreateMediator();
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
                // TODO: Log error
                Console.WriteLine("Unfortunately an error occurred");
                break;
            default:
                // TODO: Log unhandled status
                Console.WriteLine($"Unexpected result: {result.Status}");
                break;
        }
    }
}
