using Microsoft.Extensions.DependencyInjection;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Packages.Handler;

public class Handler(IServiceScopeFactory scopeFactory) : IHandler
{
    public async Task Handle<TRequest>(TRequest request, CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IHandler<TRequest>>();
        await handler.Handle(request, ct);
    }

    public async Task<TResult> Handle<TRequest, TResult>(TRequest request, CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IHandler<TRequest, TResult>>();
        return await handler.Handle(request, ct);
    }
}