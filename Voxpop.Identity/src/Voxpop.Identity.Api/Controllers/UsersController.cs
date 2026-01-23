using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Identity.Api.Dtos;
using Voxpop.Identity.Application.Handlers.Users.CreateUser;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Identity.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController(IHandler handler) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct)
    {
        await handler.Handle(new CreateUserCommand(request.PhoneNumber), ct);
        return Ok();
    }
}