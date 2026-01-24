using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;
using Voxpop.Profile.Application.Commands;

namespace Voxpop.Profile.Application.Handlers;

public class CreateProfileHandler : IHandler<CreateProfileCommand>
{
    public Task<Result> Handle(CreateProfileCommand request, CancellationToken ct)
    {
        return Task.FromResult(Result.Success());
    }
}