using Voxpop.Core.Contracts.Dtos;

namespace Voxpop.Core.Contracts.Responses;

public record GetPollsResponse(IReadOnlyList<PollSummaryDto> Items);