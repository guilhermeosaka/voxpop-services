using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Api.Requests;

public record GetPollsRequest(
    int Page = 1,
    int PageSize = 50,
    PollSortBy SortBy = PollSortBy.TotalVotesDesc,
    bool? CreatedByMe = null,
    PollStatus? Status = null,
    VoteMode? VoteMode = null,
    bool? VotedByMe = null
);