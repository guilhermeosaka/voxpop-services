using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;
using Voxpop.Template.Application.Commands;

namespace Voxpop.Template.Application.Handlers;

public class CreateTemplateHandler : IHandler<CreateTemplateCommand>
{
    public Task<Result> Handle(CreateTemplateCommand request, CancellationToken ct)
    {
        return Task.FromResult(Result.Success());
    }
}