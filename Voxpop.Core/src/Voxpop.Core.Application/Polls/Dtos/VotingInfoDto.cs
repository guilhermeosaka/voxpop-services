using Voxpop.Core.Domain.Polls.Enums;

namespace Voxpop.Core.Application.Polls.Dtos;

public record VotingInfoDto(
    VoteMode VoteMode,
    DateTimeOffset? ExpiresAt,
    bool IsClosed
);