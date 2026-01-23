using Microsoft.Extensions.DependencyInjection;
using Voxpop.Packages.Handler.Interfaces;
using Voxpop.Packages.Handler.Types;

namespace Voxpop.Packages.Handler;

public class Dispatcher(IServiceScopeFactory scopeFactory) : IDispatcher
{
    public async Task<Result> Dispatch<TRequest>(TRequest request, CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IHandler<TRequest>>();
        return await handler.Handle(request, ct);
    }

    public async Task<Result<TResult>> Dispatch<TRequest, TResult>(TRequest request, CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IHandler<TRequest, TResult>>();
        return await handler.Handle(request, ct);
    }
}