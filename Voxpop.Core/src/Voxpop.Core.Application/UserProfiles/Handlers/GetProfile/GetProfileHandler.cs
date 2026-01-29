using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Application.UserProfiles.Dtos;
using Voxpop.Core.Application.UserProfiles.Interfaces;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.UserProfiles.Handlers.GetProfile;

public class GetProfileHandler(IUserProfileQueries userProfileQueries, IRequestContext requestContext)
    : IHandler<GetProfileQuery, UserProfileDto>
{
    public async Task<Result<UserProfileDto>> Handle(GetProfileQuery request, CancellationToken ct)
    {
        var user = await userProfileQueries.GetByUserId(requestContext.UserId, request.Language);

        if (user == null)
            return Errors.UserNotFound();

        return user;
    }
}