using Voxpop.Core.Application.Polls.Dtos;

namespace Voxpop.Core.Application.Polls.Queries;

public interface IPollQueries
{
    public Task<IReadOnlyList<PollDto>> GetPollsAsync(int page, int pageSize, CancellationToken ct = default);
    public Task<VotingInfoDto> GetVotingInfoAsync(Guid pollId);
}