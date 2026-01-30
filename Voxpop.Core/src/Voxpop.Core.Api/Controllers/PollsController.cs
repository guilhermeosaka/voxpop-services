using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Core.Api.Extensions;
using Voxpop.Core.Api.Requests;
using Voxpop.Core.Application.Polls.Dtos;
using Voxpop.Core.Application.Polls.UseCases.CreatePoll;
using Voxpop.Core.Application.Polls.UseCases.GetPolls;
using Voxpop.Core.Application.Votes.UseCases.SubmitVote;
using Voxpop.Core.Application.Votes.UseCases.WithdrawVote;
using Voxpop.Core.Domain.Common;
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
            request.VoteMode,
            request.Options
        ), ct);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPollsRequest request, CancellationToken ct)
    {
        var pageSize = request.PageSize;
        if (pageSize > Constants.MaxPollsPageSize)
            pageSize = Constants.MaxPollsPageSize;
        
        var result =
            await dispatcher.Dispatch<GetPollsQuery, IReadOnlyList<PollDto>>(
                new GetPollsQuery(request.Page, pageSize), ct);
        
        return result.ToActionResult();
    }
    
    [HttpPut("{id:guid}/votes/{optionId:guid}")]
    public async Task<IActionResult> SubmitVote(Guid id, Guid optionId, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new SubmitVoteCommand(id, optionId), ct);
        
        return result.ToActionResult();
    }
    
    [HttpDelete("{id:guid}/votes/{optionId:guid}")]
    public async Task<IActionResult> WithdrawVote(Guid id, Guid optionId, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new WithdrawVoteCommand(id, optionId), ct);
        
        return result.ToActionResult();
    }
}