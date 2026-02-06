using Voxpop.Core.Application.Polls.Dtos;
using Voxpop.Core.Application.Polls.Models;
using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Application.Polls.Queries;

public interface IPollQueries
{
    public Task<IReadOnlyList<PollSummary>> GetPollsAsync(
        int page,
        int pageSize,
        PollSortBy sortBy,
        Guid? userId,
        bool? createdByMe,
        PollStatus? status,
        VoteMode? voteMode,
        bool? votedByMe,
        CancellationToken ct = default);

    public Task<VotingInfoDto?> FindVotingInfoAsync(Guid pollId, CancellationToken ct = default);
}