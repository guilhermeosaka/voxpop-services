using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Application.Polls.Models;

public record PollSummary(
    Guid Id,
    string Question,
    VoteMode VoteMode,
    DateTimeOffset? ExpiresAt,
    bool IsClosed,
    DateTimeOffset? CreatedAt,
    bool HasCreated,
    long TotalVotes,
    List<PollOptionSummary> Options);