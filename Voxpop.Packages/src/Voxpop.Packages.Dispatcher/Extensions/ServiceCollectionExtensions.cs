using Microsoft.Extensions.DependencyInjection;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Packages.Handler.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDispatcher(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(IHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
}