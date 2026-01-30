using Voxpop.Core.Domain.Polls.Enums;

namespace Voxpop.Core.Infrastructure.Persistence.Polls.Queries.Results;

public record GetPollsResult(
    Guid Id,
    string Question,
    VoteMode VoteMode,
    DateTime? ExpiresAt,
    bool IsClosed,
    DateTime CreatedAt,
    Guid OptionId,
    string OptionValue);