using Microsoft.Extensions.DependencyInjection;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Packages.Dispatcher;

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