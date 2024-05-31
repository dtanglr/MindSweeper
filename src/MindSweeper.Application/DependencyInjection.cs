using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MindSweeper.Application.Behaviors;
using MindSweeper.Application.Commands.Start;
using MindSweeper.Domain;
using MindSweeper.Domain.Results;

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

        services.AddScoped<IGameService, GameService>();
        services.AddScoped(typeof(IGameRepository), options.RepositoryType!);
        services.AddScoped(typeof(PlayerContext), (_) => new PlayerContext(Environment.MachineName));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddOpenRequestPreProcessor(typeof(PlayerContextBehavior<>));
            config.AddBehavior(typeof(IPipelineBehavior<StartCommand, Result<StartCommandResponse>>), typeof(StartCommandValidationBehavior));
        });

        services.AddValidatorsFromAssemblies([typeof(DependencyInjection).Assembly]);

        return services;
    }
}
