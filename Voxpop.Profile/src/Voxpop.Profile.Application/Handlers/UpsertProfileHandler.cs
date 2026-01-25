using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;
using Voxpop.Profile.Application.Commands;

namespace Voxpop.Profile.Application.Handlers;

public class UpsertProfileHandler : IHandler<UpsertProfileCommand>
{
    public Task<Result> Handle(UpsertProfileCommand request, CancellationToken ct)
    {
        return Task.FromResult(Result.Success());
    }
}