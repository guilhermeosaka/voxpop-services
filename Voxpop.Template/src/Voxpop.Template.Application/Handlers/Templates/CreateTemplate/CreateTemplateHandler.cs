using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Template.Application.Handlers.Templates.CreateTemplate;

public class CreateTemplateHandler : IHandler<CreateTemplateCommand>
{
    public Task<Result> Handle(CreateTemplateCommand request, CancellationToken ct)
    {
        return Task.FromResult(Result.Success());
    }
}