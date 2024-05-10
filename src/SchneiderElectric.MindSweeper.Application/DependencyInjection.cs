using Microsoft.Extensions.DependencyInjection;

namespace SchneiderElectric.MindSweeper.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMindGame(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        return services;
    }
}
