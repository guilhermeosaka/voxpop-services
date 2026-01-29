namespace Voxpop.Core.Application.Handlers.Polls.CreatePoll;

public record CreatePollCommand(string Question, DateTimeOffset? ExpiresAt, List<CreatePollOptionDto> Options);