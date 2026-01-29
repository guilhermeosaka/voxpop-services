using Voxpop.Core.Application.Handlers.Polls.CreatePoll;

namespace Voxpop.Core.Api.Requests;

public record CreatePollRequest(string Question, DateTimeOffset? ExpiresAt, List<CreatePollOptionDto> Options);