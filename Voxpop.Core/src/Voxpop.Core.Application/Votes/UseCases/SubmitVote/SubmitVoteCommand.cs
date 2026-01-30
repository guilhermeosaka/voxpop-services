namespace Voxpop.Core.Application.Votes.UseCases.SubmitVote;

public record SubmitVoteCommand(Guid PollId, Guid OptionId);