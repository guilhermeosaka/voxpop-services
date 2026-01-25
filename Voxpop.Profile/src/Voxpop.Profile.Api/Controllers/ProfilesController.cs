using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Profile.Api.Extensions;
using Voxpop.Profile.Api.Requests;
using Voxpop.Profile.Application.Commands;

namespace Voxpop.Profile.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProfilesController(IDispatcher dispatcher) : ControllerBase
{
    [AllowAnonymous]
    [HttpPut]
    public async Task<IActionResult> Upsert([FromBody] UpsertProfileRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new UpsertProfileCommand(), ct);
        return result.ToActionResult();
    }
}