using Microsoft.Extensions.DependencyInjection;
using Voxpop.Identity.Application.Common;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Services;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Identity.Application.Handlers.Tokens.CreateToken;

public class CreateTokenHandler(
    IServiceProvider serviceProvider,
    ITokenGenerator tokenGenerator,
    RefreshTokenService refreshTokenService
    ) : IHandler<CreateTokenCommand, CreateTokenResult>
{
    public async Task<Result<CreateTokenResult>> Handle(CreateTokenCommand request, CancellationToken ct)
    {
        var codeService = serviceProvider.GetRequiredKeyedService<ICodeService>(request.Channel);
        
        if (!await codeService.VerifyAsync(request.Target,request.Code, ct))
            return Errors.InvalidCode(request.Code);
        
        var userFinder = serviceProvider.GetRequiredKeyedService<IUserFinder>(request.Channel);
        var user = await userFinder.FindAsync(request.Target, ct);

        if (user == null)
            return Errors.UserNotFound(request.Target, request.Channel);

        var accessToken = tokenGenerator.Generate(user);
        var refreshToken = await refreshTokenService.CreateAsync(user.Id, ct: ct);
        
        return new CreateTokenResult(accessToken, refreshToken!);
    }
}