using Voxpop.Packages.Handler.Types;

namespace Voxpop.Packages.Handler.Interfaces;

public interface IDispatcher
{
    Task<Result> Dispatch<TRequest>(TRequest request, CancellationToken ct);
    Task<Result<TResult>> Dispatch<TRequest, TResult>(TRequest request, CancellationToken ct);
}