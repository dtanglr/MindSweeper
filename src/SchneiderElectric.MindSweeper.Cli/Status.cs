using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchneiderElectric.MindSweeper.Application.Requests.GetGame;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    public static CliCommand StatusCommand => new("status", "Get the status of the current game")
    {
        Action = CommandHandler.Create<IHost>(async (host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var request = new GetGameRequest(Environment.MachineName);
            var result = await mediator.Send(request);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                case ResultStatus.Ok:
                    var game = result.Value!.Game;
                    Console.WriteLine("You currently have an active game!");
                    Console.WriteLine($"The field of play contains {game.Settings.Bombs} bombs on {game.Settings.Squares} squares ({game.Settings.Columns} columns by {game.Settings.Rows} rows).");
                    Console.WriteLine($"You are currently on square: {game.CurrentSquare}");
                    Console.WriteLine($"Based on your current square, you can move: {string.Join(", ", game.AvailableMoves.Select(m => m.Key.ToString()))}");
                    Console.WriteLine($"You have made {game.Moves} moves so far and have {game.Lives} {(game.Lives > 1 ? "lives" : "life")} left in this game.");
                    break;
                case ResultStatus.NotFound:
                    Console.WriteLine("You don't have an active game. Start a new game to play.");
                    break;
                case ResultStatus.Invalid:
                    Console.WriteLine("One or more validation issues occurred.");
                    result.ValidationIssues.ForEach(e => Console.WriteLine(e.Message));
                    break;
                case ResultStatus.Error:
                    Console.WriteLine("Unfortunately an error occurred.");
                    result.Errors.ForEach(e => Console.WriteLine(e));
                    break;
                default:
                    Console.WriteLine($"Unexpected result: {result.Status}");
                    break;
            }

            Console.WriteLine();
        })
    };
}
