using Voxpop.Core.Application.Polls.Dtos;
using Voxpop.Core.Application.Polls.Models;

namespace Voxpop.Core.Application.Polls.Queries;

public interface IPollQueries
{
    public Task<IReadOnlyList<PollSummary>> GetPollsAsync(
        int page, 
        int pageSize, 
        Guid? userId,
        CancellationToken ct = default);

    public Task<VotingInfoDto?> FindVotingInfoAsync(Guid pollId, CancellationToken ct = default);
}