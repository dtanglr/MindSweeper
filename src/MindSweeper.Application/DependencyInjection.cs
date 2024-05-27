using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MindSweeper.Application.Commands.End;
using MindSweeper.Application.Commands.Move;
using MindSweeper.Application.Commands.Start;
using MindSweeper.Application.Requests.GetGame;
using MindSweeper.Domain;

namespace MindSweeper.Application;

/// <summary>
/// Provides dependency injection configuration for the MindSweeper game application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the MindSweeper game services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="build">An action to configure the game options.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddMindGame(this IServiceCollection services, Action<GameOptions> build)
    {
        var options = new GameOptions();
        build.Invoke(options);

        services.AddScoped(typeof(IGameRepository), options.RepositoryType!);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddBehavior(typeof(IPipelineBehavior<EndCommand, Result>), typeof(EndCommandValidationBehavior));
            config.AddBehavior(typeof(IPipelineBehavior<MoveCommand, Result<MoveCommandResponse>>), typeof(MoveCommandGetGameBehavior));
            config.AddBehavior(typeof(IPipelineBehavior<MoveCommand, Result<MoveCommandResponse>>), typeof(MoveCommandValidationBehavior));
            config.AddBehavior(typeof(IPipelineBehavior<StartCommand, Result<StartCommandResponse>>), typeof(StartCommandValidationBehavior));
            config.AddBehavior(typeof(IPipelineBehavior<GetGameRequest, Result<GetGameRequestResponse>>), typeof(GetGameRequestValidationBehavior));
        });

        services.AddValidatorsFromAssemblies([typeof(DependencyInjection).Assembly]);

        return services;
    }
}
