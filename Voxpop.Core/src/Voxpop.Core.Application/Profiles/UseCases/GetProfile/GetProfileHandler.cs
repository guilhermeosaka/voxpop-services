using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Application.Profiles.Models;
using Voxpop.Core.Application.Profiles.Queries;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Profiles.UseCases.GetProfile;

public class GetProfileHandler(IProfileQueries queries, IRequestContext requestContext)
    : IHandler<GetProfileQuery, ProfileSummary>
{
    public async Task<Result<ProfileSummary>> Handle(GetProfileQuery request, CancellationToken ct)
    {
        var user = await queries.GetByUserIdAsync(requestContext.UserId, request.Language);

        if (user == null)
            return Errors.UserNotFound();

        return user;
    }
}