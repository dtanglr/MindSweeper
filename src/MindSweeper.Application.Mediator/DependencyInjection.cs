using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MindSweeper.Application.Mediator.Behaviors;
using MindSweeper.Application.Mediator.Commands.Start;

namespace MindSweeper.Application.Mediator;

/// <summary>
/// Provides dependency injection configuration for the MindSweeper application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds MediatR and FluentValidation services to the game configurator.
    /// </summary>
    /// <param name="configurator">The game configurator.</param>
    /// <returns>The updated game configurator.</returns>
    public static GameConfigurator UseMediatorPipeline(this GameConfigurator configurator)
    {
        configurator.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddOpenRequestPreProcessor(typeof(PlayerContextBehavior<>));
            config.AddBehavior(typeof(IPipelineBehavior<StartCommandRequest, Result<StartCommandResponse>>), typeof(StartCommandRequestValidationBehavior));
        });

        configurator.Services.AddValidatorsFromAssemblies([typeof(DependencyInjection).Assembly]);

        return configurator;
    }
}
