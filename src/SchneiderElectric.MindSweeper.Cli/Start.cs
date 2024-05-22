using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchneiderElectric.MindSweeper.Application.Commands.Start;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    public static CliCommand StartCommand => new("start", "Start a new game")
    {
        Options =
        {
            new CliOption<int>("--columns", "-c")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = "The number of columns",
                HelpName = "columns",
                Required = false,
                DefaultValueFactory = (arg) => Settings.DefaultColumns
            },
            new CliOption<int>("--rows", "-r")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = "The number of rows",
                HelpName = "rows",
                Required = false,
                DefaultValueFactory = (arg) => Settings.DefaultRows
            },
            new CliOption<int>("--bombs", "-b")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = "The number of bombs",
                HelpName = "bombs",
                Required = false
            },
            new CliOption<int>("--lives", "-l")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = "The number of lives",
                HelpName = "lives",
                Required = false,
                DefaultValueFactory = (arg) => Settings.DefaultLives
            }
        },
        Action = CommandHandler.Create<StartOptions, IHost>(async (options, host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var command = new StartCommand(Environment.MachineName, options.Settings);
            var result = await mediator.Send(command);

            Console.WriteLine();

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
        }),
    };
}
