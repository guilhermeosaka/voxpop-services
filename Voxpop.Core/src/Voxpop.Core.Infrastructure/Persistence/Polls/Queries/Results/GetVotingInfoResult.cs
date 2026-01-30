using Voxpop.Core.Domain.Polls.Enums;

namespace Voxpop.Core.Infrastructure.Persistence.Polls.Queries.Results;

public record GetVotingInfoResult(
    VoteMode VoteMode,
    DateTime? ExpiresAt,
    bool IsClosed
);