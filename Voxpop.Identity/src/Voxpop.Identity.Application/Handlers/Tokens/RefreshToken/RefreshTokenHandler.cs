using Voxpop.Identity.Application.Common;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Services;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Identity.Application.Handlers.Tokens.RefreshToken;

public class RefreshTokenHandler(
    IUserRepository userRepository,
    RefreshTokenService refreshTokenService,
    ITokenGenerator tokenGenerator)
    : IHandler<RefreshTokenCommand, RefreshTokenResult>
{
    public async Task<Result<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var (userId, token) = await refreshTokenService.RefreshAsync(request.Token, ct);

        if (userId == null || token == null)
            return Errors.InvalidRefreshToken(request.Token);

        var user = await userRepository.FindByIdAsync(userId.Value);
        
        if (user == null)
            return Errors.UserNotFound(userId.Value);
        
        var accessToken = tokenGenerator.Generate(user);

        return new RefreshTokenResult(accessToken, token);
    }
}