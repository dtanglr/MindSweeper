using MindSweeper.Application.Commands.End;

namespace MindSweeper.Cli;

partial class Program
{
    /// <summary>
    /// Represents the CLI command for ending the game.
    /// </summary>
    public static CliCommand EndCommand => new("end", Resources.EndCommandDescription)
    {
        Action = CommandHandler.Create<IHost>(async (host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var command = new EndCommand(Environment.MachineName);
            var result = await mediator.Send(command).ConfigureAwait(false);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                    Console.WriteLine(Resources.EndCommandResultStatusAccepted);
                    break;
                case ResultStatus.NotFound:
                    Console.WriteLine(Resources.EndCommandResultStatusNotFound);
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
        })
    };
}
