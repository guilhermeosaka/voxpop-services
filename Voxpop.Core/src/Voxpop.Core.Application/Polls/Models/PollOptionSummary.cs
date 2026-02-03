namespace Voxpop.Core.Application.Polls.Models;

public record PollOptionSummary(Guid Id, string Value, long Votes, bool HasVoted);