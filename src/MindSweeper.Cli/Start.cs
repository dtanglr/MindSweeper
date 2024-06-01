using MindSweeper.Application.Mediator.Commands.Start;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli;

partial class Program
{
    /// <summary>
    /// Represents the CLI command for starting a new game.
    /// </summary>
    public static CliCommand StartCommand => new("start", Resources.StartCommandDescription)
    {
        Options =
        {
            new CliOption<int>("--columns", "-c")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = Resources.StartCommandColumnsOptionDescription,
                HelpName = Resources.StartCommandColumnsOptionHelpName,
                Required = false,
                DefaultValueFactory = (arg) => GameSettings.DefaultColumns
            },
            new CliOption<int>("--rows", "-r")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = Resources.StartCommandRowsOptionDescription,
                HelpName = Resources.StartCommandRowsOptionHelpName,
                Required = false,
                DefaultValueFactory = (arg) => GameSettings.DefaultRows
            },
            new CliOption<int>("--bombs", "-b")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = Resources.StartCommandBombsOptionDescription,
                HelpName = Resources.StartCommandBombsOptionHelpName,
                Required = false
            },
            new CliOption<int>("--lives", "-l")
            {
                Arity = ArgumentArity.ExactlyOne,
                Description = Resources.StartCommandLivesOptionDescription,
                HelpName = Resources.StartCommandLivesOptionHelpName,
                Required = false,
                DefaultValueFactory = (arg) => GameSettings.DefaultLives
            }
        },
        Action = CommandHandler.Create<StartOptions, IHost>(async (options, host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var command = new StartCommand(options.Settings);
            var result = await mediator.Send(command);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                    var game = result.Value!.Game;
                    Console.WriteLine(Resources.StartCommandResultStatusAccepted);
                    Console.WriteLine();
                    Console.WriteLine(game.GetGameStatus());
                    break;
                case ResultStatus.Conflict:
                    Console.WriteLine(Resources.StartCommandResultStatusConflict);
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
        }),
    };
}
