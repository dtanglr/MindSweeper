using Microsoft.Extensions.Hosting;
using SchneiderElectric.MindSweeper.Application.Commands.Start;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    private static async Task Start(StartOptions options, IHost host)
    {
        var mediator = host.CreateMediator();
        var command = new StartCommand(Environment.MachineName, options.Settings);
        var result = await mediator.Send(command);

        switch (result.Status)
        {
            case ResultStatus.Accepted:
            case ResultStatus.Ok:
                var game = result.Value!.Game;
                Console.WriteLine("Successfully started a new game.");
                Console.WriteLine($"The field of play contains {game.Settings.Bombs} bombs on {game.Settings.Squares} squares ({game.Settings.Columns} columns by {game.Settings.Rows} rows).");
                Console.WriteLine($"You are starting on square: {game.CurrentSquare}.");
                Console.WriteLine($"Based on your current square, you can move: {string.Join(", ", game.AvailableMoves.Select(m => m.Key.ToString()))}");
                Console.WriteLine($"You have {game.Settings.Lives} lives. God speed!");
                break;
            case ResultStatus.Conflict:
                Console.WriteLine("You already have an active game. Please end the current game before starting a new one.");
                break;
            case ResultStatus.Error:
                // TODO: Log error
                Console.WriteLine("Unfortunately an error occurred.");
                break;
            default:
                // TODO: Log unhandled status
                Console.WriteLine($"Unexpected result: {result.Status}");
                break;
        }
    }
}
