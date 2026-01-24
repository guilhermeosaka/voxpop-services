using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Template.Api.Extensions;
using Voxpop.Template.Api.Requests;
using Voxpop.Template.Application.Commands;

namespace Voxpop.Template.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TemplatesController(IDispatcher dispatcher) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTemplateRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new CreateTemplateCommand(), ct);
        return result.ToActionResult();
    }
}