using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Identity.Api.Dtos;
using Voxpop.Identity.Application.Handlers.Verifications.CreatePhoneCode;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Identity.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class VerificationsController(IHandler handler) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("phone")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreatePhoneCodeRequest request, CancellationToken ct)
    {
        await handler.Handle(new CreatePhoneCodeCommand(request.PhoneNumber), ct);
        return Ok();
    }
}