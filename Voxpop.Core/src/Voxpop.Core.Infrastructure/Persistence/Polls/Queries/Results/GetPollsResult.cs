using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Infrastructure.Persistence.Polls.Queries.Results;

public record GetPollsResult(
    Guid Id,
    string Question,
    VoteMode VoteMode,
    DateTime? ExpiresAt,
    bool IsClosed,
    DateTime CreatedAt,
    bool HasCreated,
    long TotalVotes,
    bool ResultsAreVisible,
    Guid OptionId,
    string OptionValue,
    long? OptionVotes,
    bool OptionHasVoted);