using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Core.Api.Extensions;
using Voxpop.Core.Api.Mappers;
using Voxpop.Core.Api.Requests;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Application.Profiles.Models;
using Voxpop.Core.Application.Profiles.UseCases.GetProfile;
using Voxpop.Core.Application.Profiles.UseCases.SaveProfile;
using Voxpop.Packages.Dispatcher.Interfaces;

namespace Voxpop.Core.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ProfilesController(IDispatcher dispatcher, IRequestContext requestContext) : ControllerBase
{
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Save(SaveProfileRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new SaveProfileCommand(
            request.PersonalInfo,
            request.LocationInfo,
            request.ProfessionalInfo,
            request.CulturalInfo
        ), ct);

        return result.ToActionResult();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var result =
            await dispatcher.Dispatch<GetProfileQuery, ProfileSummary>(new GetProfileQuery(requestContext.Language),
                ct);

        return result.ToActionResult(GetProfileMapper.ToResponse);
    }
}