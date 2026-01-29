using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Dtos;
using Voxpop.Core.Application.Interfaces;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Handlers.Profiles.GetProfile;

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