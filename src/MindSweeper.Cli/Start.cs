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
        Action = CommandHandler.Create<StartOptions, IConsole, IMediator>(async (options, console, mediator) =>
        {
            var command = new StartCommand(options.Settings);
            var result = await mediator.Send(command);

            console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                    var game = result.Value!.Game;
                    console.WriteLine(Resources.StartCommandResultStatusAccepted);
                    console.WriteLine();
                    console.WriteGameStatus(game);
                    break;
                case ResultStatus.Conflict:
                    console.WriteLine(Resources.StartCommandResultStatusConflict);
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
        }),
    };
}
