using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Application.Polls.UseCases.GetPolls;

public record GetPollsQuery(
    int Page,
    int PageSize,
    PollSortBy SortBy,
    bool? CreatedByMe,
    PollStatus? Status,
    VoteMode? VoteMode,
    bool? VotedByMe
);