using Microsoft.Extensions.DependencyInjection;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Identity.Application.Handlers.Codes.CreateCode;

public class CreateCodeHandler(
    IServiceProvider serviceProvider)
    : IHandler<CreateCodeCommand>
{
    public async Task<Result> Handle(CreateCodeCommand request, CancellationToken ct)
    {
        var codeService = serviceProvider.GetRequiredKeyedService<ICodeService>(request.Channel);
        await codeService.SendAsync(request.Target, ct);

        return Result.Success();
    }
}