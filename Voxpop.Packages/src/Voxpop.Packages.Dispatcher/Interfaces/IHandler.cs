using Voxpop.Packages.Handler.Types;

namespace Voxpop.Packages.Handler.Interfaces;

public interface IHandler<in TRequest>
{
    Task<Result> Handle(TRequest request, CancellationToken ct);
}

public interface IHandler<in TRequest, TResult>
{
    Task<Result<TResult>> Handle(TRequest request, CancellationToken ct);
}

