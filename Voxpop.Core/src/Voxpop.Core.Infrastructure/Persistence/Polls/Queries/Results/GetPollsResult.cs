namespace Voxpop.Core.Infrastructure.Persistence.Polls.Queries.Results;

public record GetPollsResult(
    Guid Id,
    string Question,
    DateTime? ExpiresAt,
    Guid OptionId,
    string OptionValue,
    DateTime CreatedAt);