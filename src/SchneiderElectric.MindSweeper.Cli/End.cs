using Microsoft.Extensions.Hosting;
using SchneiderElectric.MindSweeper.Application.Commands.End;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    private static async Task End(IHost host)
    {
        var mediator = host.CreateMediator();
        var command = new EndCommand(Environment.MachineName);
        var result = await mediator.Send(command);

        switch (result.Status)
        {
            case ResultStatus.Accepted:
            case ResultStatus.Ok:
                Console.WriteLine("Successfully ended your game.");
                break;
            case ResultStatus.NotFound:
                Console.WriteLine("There is no game to end!");
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
