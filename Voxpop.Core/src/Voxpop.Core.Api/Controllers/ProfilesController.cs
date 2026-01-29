using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Core.Api.Extensions;
using Voxpop.Core.Api.Requests;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Application.UserProfiles.Dtos;
using Voxpop.Core.Application.UserProfiles.Handlers.GetProfile;
using Voxpop.Core.Application.UserProfiles.Handlers.UpsertProfile;
using Voxpop.Packages.Dispatcher.Interfaces;

namespace Voxpop.Core.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/[controller]")]
public class ProfilesController(IDispatcher dispatcher, IRequestContext requestContext) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> Upsert(UpsertProfileRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new UpsertProfileCommand(
            request.PersonalInfo,
            request.LocationInfo,
            request.ProfessionalInfo,
            request.CulturalInfo
        ), ct);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var result =
            await dispatcher.Dispatch<GetProfileQuery, UserProfileDto>(new GetProfileQuery(requestContext.Language),
                ct);

        return result.ToActionResult();
    }
}