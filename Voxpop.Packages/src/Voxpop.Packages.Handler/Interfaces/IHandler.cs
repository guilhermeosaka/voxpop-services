namespace Voxpop.Packages.Handler.Interfaces;

public interface IHandler
{
    Task Handle<TRequest>(TRequest request, CancellationToken ct);
    Task<TResult> Handle<TRequest, TResult>(TRequest request, CancellationToken ct);
}

public interface IHandler<in TRequest>
{
    Task Handle(TRequest request, CancellationToken ct);
}

public interface IHandler<in TRequest, TResult> 
{
    Task<TResult> Handle(TRequest request, CancellationToken ct);
}