using MindSweeper.Domain.Results;
using Request = MindSweeper.Application.Mediator.Commands.Start.StartCommand;

namespace MindSweeper.Cli.Commands.Start;

/// <summary>
/// Represents a command to start the game.
/// </summary>
public class StartCommand : CliCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommand"/> class.
    /// </summary>
    public StartCommand() : base("start", Resources.StartCommandDescription)
    {
        // Add options
        Options.Add(new ColumnsOption());
        Options.Add(new RowsOption());
        Options.Add(new BombsOption());
        Options.Add(new LivesOption());

        //Add action
        Action = CommandHandler.Create<StartOptions, IConsole, IMediator>(async (options, console, mediator) =>
        {
            var command = new Request(options.Settings);
            var result = await mediator.Send(command);

            console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                    var game = result.Value!.Game;
                    console.WriteLine(Resources.StartCommandResultStatusAccepted);
                    console.WriteLine();
                    console.Write(game);
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
        });
    }
}
