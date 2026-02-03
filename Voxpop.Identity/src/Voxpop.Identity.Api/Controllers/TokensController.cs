using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Identity.Api.Dtos;
using Voxpop.Identity.Api.Extensions;
using Voxpop.Identity.Application.Handlers.Tokens.CreateToken;
using Voxpop.Identity.Application.Handlers.Tokens.RefreshToken;
using Voxpop.Packages.Dispatcher.Interfaces;

namespace Voxpop.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TokensController(IDispatcher dispatcher) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTokenRequest request, CancellationToken ct)
    {
        var result =
            await dispatcher.Dispatch<CreateTokenCommand, CreateTokenResult>(
                new CreateTokenCommand(request.Target, request.Channel, request.Code), ct);

        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpPut]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken ct)
    {
        var result =
            await dispatcher.Dispatch<RefreshTokenCommand, RefreshTokenResult>(
                new RefreshTokenCommand(request.Token), ct);

        return result.ToActionResult();
    }
}