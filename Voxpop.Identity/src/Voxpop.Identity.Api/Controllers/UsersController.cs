using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Identity.Api.Dtos;
using Voxpop.Identity.Api.Extensions;
using Voxpop.Identity.Application.Handlers.Users.CreateUser;
using Voxpop.Packages.Dispatcher.Interfaces;

namespace Voxpop.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IDispatcher dispatcher) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new CreateUserCommand(request.PhoneNumber), ct);
        return result.ToActionResult();
    }
}