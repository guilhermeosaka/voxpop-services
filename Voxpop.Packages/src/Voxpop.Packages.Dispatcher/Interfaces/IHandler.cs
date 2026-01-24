using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Packages.Dispatcher.Interfaces;

public interface IHandler<in TRequest>
{
    Task<Result> Handle(TRequest request, CancellationToken ct);
}

public interface IHandler<in TRequest, TResult>
{
    Task<Result<TResult>> Handle(TRequest request, CancellationToken ct);
}

