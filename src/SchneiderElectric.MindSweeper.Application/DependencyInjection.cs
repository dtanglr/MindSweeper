using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchneiderElectric.MindSweeper.Application.Behaviors;
using SchneiderElectric.MindSweeper.Application.Commands.Move;
using SchneiderElectric.MindSweeper.Application.Commands.Start;
using SchneiderElectric.MindSweeper.Application.Requests.GetGame;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMindGame(this IServiceCollection services, Action<GameOptions> build)
    {
        var options = new GameOptions();
        build.Invoke(options);

        services.AddScoped(typeof(IGameRepository), options.RepositoryType!);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ResultValidationBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(MoveCommandValidationBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(StartCommandValidationBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(GetGameRequestValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblies([typeof(DependencyInjection).Assembly]);

        return services;
    }
}
