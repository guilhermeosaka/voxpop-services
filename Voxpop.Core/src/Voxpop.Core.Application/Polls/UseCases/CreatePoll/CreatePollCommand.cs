using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Application.Polls.UseCases.CreatePoll;

public record CreatePollCommand(
    string Question,
    DateTimeOffset? ExpiresAt,
    VoteMode VoteMode,
    IReadOnlyList<CreatePollOptionDto> Options);