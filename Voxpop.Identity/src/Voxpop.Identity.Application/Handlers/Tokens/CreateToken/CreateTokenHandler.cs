using Microsoft.Extensions.DependencyInjection;
using Voxpop.Identity.Application.Common;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Services;
using Voxpop.Packages.Handler.Interfaces;
using Voxpop.Packages.Handler.Types;

namespace Voxpop.Identity.Application.Handlers.Tokens.CreateToken;

public class CreateTokenHandler(
    CodeVerifier codeVerifier,
    IServiceProvider serviceProvider,
    ITokenGenerator tokenGenerator
    ) : IHandler<CreateTokenCommand, CreateTokenResult>
{
    public async Task<Result<CreateTokenResult>> Handle(CreateTokenCommand request, CancellationToken ct)
    {
        if (!await codeVerifier.VerifyAsync(request.Target, request.Channel, request.Code, ct))
            return Errors.InvalidCode(request.Code);
        
        var userFinder = serviceProvider.GetRequiredKeyedService<IUserFinder>(request.Channel);
        var user = await userFinder.FindAsync(request.Target);

        if (user == null)
            return Errors.UserNotFound(request.Target, request.Channel);

        return new CreateTokenResult(tokenGenerator.Generate(user));
    }
}