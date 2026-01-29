using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Core.Api.Extensions;
using Voxpop.Core.Api.Requests;
using Voxpop.Core.Application.Polls.Handlers.CreatePoll;
using Voxpop.Packages.Dispatcher.Interfaces;

namespace Voxpop.Core.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/[controller]")]
public class PollsController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreatePollRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new CreatePollCommand(
            request.Question,
            request.ExpiresAt,
            request.Options
        ), ct);

        return result.ToActionResult();
    }
}