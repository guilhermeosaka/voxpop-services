using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Identity.Api.Dtos;
using Voxpop.Identity.Application.Handlers.Codes.CreateCode;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Identity.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CodesController(IHandler handler) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCodeRequest request, CancellationToken ct)
    {
        await handler.Handle(new CreateCodeCommand(request.Target, request.Channel), ct);
        return Ok();
    }
}