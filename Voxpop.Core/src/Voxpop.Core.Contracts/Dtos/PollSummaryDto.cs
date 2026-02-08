using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Contracts.Dtos;

public record PollSummaryDto(    
    Guid Id,
    string Question,
    VoteMode VoteMode,
    DateTimeOffset? ExpiresAt,
    bool IsClosed,
    DateTimeOffset? CreatedAt,
    bool HasCreated,
    long TotalVotes,
    List<PollOptionSummaryDto> Options);