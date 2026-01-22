using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Identity.Api.Contracts;
using Voxpop.Identity.Application.Commands;
using Voxpop.Identity.Application.Exceptions;
using Voxpop.Identity.Application.Interfaces;

namespace Voxpop.Identity.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController(IHandler<RegisterUserCommand> registerUser) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request, CancellationToken ct)
    {
        try
        {
            await registerUser.Handle(new RegisterUserCommand(request.PhoneNumber), ct);
            
            return Ok();
        }
        catch (UserAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
    }
}