using Voxpop.Core.Application.Polls.Models;
using Voxpop.Core.Contracts.Dtos;
using Voxpop.Core.Contracts.Responses;

namespace Voxpop.Core.Api.Mappers;

public static class GetPollsMapper
{
    public static GetPollsResponse ToResponse(this IReadOnlyList<PollSummary> polls) =>
        new(polls.Select(p => new PollSummaryDto(
            p.Id,
            p.Question,
            p.VoteMode,
            p.ExpiresAt,
            p.IsClosed,
            p.CreatedAt,
            p.Options.Select(o => new PollOptionSummaryDto(o.Id, o.Value)).ToList())).ToList());
}