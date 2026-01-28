using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;
using Voxpop.Profile.Application.Dtos;
using Voxpop.Profile.Application.Interfaces;

namespace Voxpop.Profile.Application.Handlers.Profiles.GetProfile;

public class GetProfileHandler(IUserProfileQueries userProfileQueries, IRequestContext requestContext)
    : IHandler<GetProfileQuery, UserProfileDto>
{
    public async Task<Result<UserProfileDto>> Handle(GetProfileQuery request, CancellationToken ct)
    {
        if (!requestContext.UserId.HasValue)
            throw new UnauthorizedAccessException();

        var userId = requestContext.UserId.Value;
        
        var user = await userProfileQueries.GetByUserId(userId);

        return user;
    }
}