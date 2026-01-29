using Voxpop.Core.Application.Polls.Handlers.CreatePoll;

namespace Voxpop.Core.Api.Requests;

public record CreatePollRequest(string Question, DateTimeOffset? ExpiresAt, List<CreatePollOptionDto> Options);