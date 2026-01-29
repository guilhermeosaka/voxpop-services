namespace Voxpop.Core.Application.Polls.Handlers.CreatePoll;

public record CreatePollCommand(string Question, DateTimeOffset? ExpiresAt, List<CreatePollOptionDto> Options);