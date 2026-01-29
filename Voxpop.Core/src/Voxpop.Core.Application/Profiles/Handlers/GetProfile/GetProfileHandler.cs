using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Application.Profiles.Dtos;
using Voxpop.Core.Application.Profiles.Queries;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Profiles.Handlers.GetProfile;

public class GetProfileHandler(IUserProfileQueries queries, IRequestContext requestContext)
    : IHandler<GetProfileQuery, UserProfileDto>
{
    public async Task<Result<UserProfileDto>> Handle(GetProfileQuery request, CancellationToken ct)
    {
        var user = await queries.GetByUserIdAsync(requestContext.UserId, request.Language);

        if (user == null)
            return Errors.UserNotFound();

        return user;
    }
}