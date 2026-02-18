using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Application.Polls.UseCases.CreatePoll;

public record CreatePollCommand(
    string Question,
    DateTimeOffset? ExpiresAt,
    VoteMode VoteMode,
    PollAccess Access,
    PollResultsAccess ResultsAccess,
    PollResultsVisibility ResultsVisibility,
    IReadOnlyList<CreatePollOptionDto> Options);