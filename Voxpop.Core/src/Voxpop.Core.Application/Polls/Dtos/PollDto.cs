using Voxpop.Core.Domain.Polls.Enums;

namespace Voxpop.Core.Application.Polls.Dtos;

public record PollDto(
    Guid Id,
    string Question,
    VoteMode VoteMode,
    DateTimeOffset? ExpiresAt,
    bool IsClosed,
    DateTimeOffset? CreatedAt,
    List<PollOptionDto> Options);