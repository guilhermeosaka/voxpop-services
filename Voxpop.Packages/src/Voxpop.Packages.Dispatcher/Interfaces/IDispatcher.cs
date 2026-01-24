using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Packages.Dispatcher.Interfaces;

public interface IDispatcher
{
    Task<Result> Dispatch<TRequest>(TRequest request, CancellationToken ct);
    Task<Result<TResult>> Dispatch<TRequest, TResult>(TRequest request, CancellationToken ct);
}