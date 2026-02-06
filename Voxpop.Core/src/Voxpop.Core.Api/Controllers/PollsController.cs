using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Core.Api.Extensions;
using Voxpop.Core.Api.Mappers;
using Voxpop.Core.Api.Requests;
using Voxpop.Core.Application.Polls.Models;
using Voxpop.Core.Application.Polls.UseCases.CreatePoll;
using Voxpop.Core.Application.Polls.UseCases.GetPolls;
using Voxpop.Core.Application.Reactions.UseCases.SubmitReaction;
using Voxpop.Core.Application.Reactions.UseCases.WithdrawReaction;
using Voxpop.Core.Application.Votes.UseCases.SubmitVote;
using Voxpop.Core.Application.Votes.UseCases.WithdrawVote;
using Voxpop.Core.Contracts.Responses;
using Voxpop.Core.Domain.Common;
using Voxpop.Core.Domain.Votes.Enums;
using Voxpop.Packages.Dispatcher.Interfaces;

namespace Voxpop.Core.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class PollsController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreatePollResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreatePollRequest request, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch<CreatePollCommand, Guid>(
            new CreatePollCommand(
                request.Question,
                request.ExpiresAt,
                request.VoteMode,
                request.Options
            ), ct);

        return result.ToCreatedResult(nameof(GetById), new { id = result.Value }, CreatePollMapper.ToResponse);
    }

    [HttpGet("{id:guid}", Name = nameof(GetById))]
    public Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(GetPollsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetPollsRequest request, CancellationToken ct)
    {
        var pageSize = request.PageSize;
        if (pageSize > Constants.MaxPollsPageSize)
            pageSize = Constants.MaxPollsPageSize;

        var result =
            await dispatcher.Dispatch<GetPollsQuery, IReadOnlyList<PollSummary>>(
                new GetPollsQuery(
                    request.Page,
                    pageSize,
                    request.SortBy,
                    request.CreatedByMe,
                    request.Status,
                    request.VoteMode,
                    request.VotedByMe), ct);

        return result.ToActionResult(GetPollsMapper.ToResponse);
    }

    [HttpPut("{id:guid}/votes/{optionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SubmitVote(Guid id, Guid optionId, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new SubmitVoteCommand(id, optionId), ct);

        return result.ToActionResult();
    }

    [HttpDelete("{id:guid}/votes/{optionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> WithdrawVote(Guid id, Guid optionId, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new WithdrawVoteCommand(id, optionId), ct);

        return result.ToActionResult();
    }

    [HttpPut("{id:guid}/reactions/{reactionType}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmitReaction(Guid id, ReactionType reactionType, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new SubmitReactionCommand(id, reactionType), ct);

        return result.ToActionResult();
    }

    [HttpDelete("{id:guid}/reactions/{reactionType}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> WithdrawReaction(Guid id, ReactionType reactionType, CancellationToken ct)
    {
        var result = await dispatcher.Dispatch(new WithdrawReactionCommand(id, reactionType), ct);

        return result.ToActionResult();
    }
}