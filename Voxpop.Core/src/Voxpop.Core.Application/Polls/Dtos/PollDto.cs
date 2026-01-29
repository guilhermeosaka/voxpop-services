namespace Voxpop.Core.Application.Polls.Dtos;

public record PollDto(
    Guid Id,
    string Question,
    DateTimeOffset? ExpiresAt,
    List<PollOptionDto> Options,
    DateTimeOffset? CreatedAt);