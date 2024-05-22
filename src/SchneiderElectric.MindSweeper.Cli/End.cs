using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchneiderElectric.MindSweeper.Application.Commands.End;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Cli;

partial class Program
{
    public static CliCommand EndCommand => new("end", "End the current game")
    {
        Action = CommandHandler.Create<IHost>(async (host) =>
        {
            var mediator = host.Services.GetRequiredService<IMediator>();
            var command = new EndCommand(Environment.MachineName);
            var result = await mediator.Send(command);

            Console.WriteLine();

            switch (result.Status)
            {
                case ResultStatus.Accepted:
                case ResultStatus.Ok:
                    Console.WriteLine("Successfully ended your game.");
                    break;
                case ResultStatus.NotFound:
                    Console.WriteLine("There is no game to end!");
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
        })
    };
}
