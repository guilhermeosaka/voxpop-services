using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Profile.Api.Extensions;
using Voxpop.Profile.Api.Requests;
using Voxpop.Profile.Application.Handlers.Profiles.UpsertProfile;

namespace Voxpop.Profile.Api.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class ProfilesController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> Upsert([FromBody] UpsertProfileRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new UpsertProfileCommand(
            request.PersonalInfo,
            request.LocationInfo,
            request.ProfessionalInfo,
            request.CulturalInfo
        ), ct);
        
        return result.ToActionResult();
    }
}