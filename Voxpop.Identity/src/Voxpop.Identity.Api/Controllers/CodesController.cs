using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Identity.Api.Dtos;
using Voxpop.Identity.Api.Extensions;
using Voxpop.Identity.Application.Handlers.Codes.CreateCode;
using Voxpop.Packages.Dispatcher.Interfaces;

namespace Voxpop.Identity.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CodesController(IDispatcher dispatcher) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCodeRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new CreateCodeCommand(request.Target, request.Channel), ct);
        
        return result.ToActionResult();
    }
}