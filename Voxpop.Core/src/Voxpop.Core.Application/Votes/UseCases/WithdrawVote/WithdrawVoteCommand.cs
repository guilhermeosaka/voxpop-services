namespace Voxpop.Core.Application.Votes.UseCases.WithdrawVote;

public record WithdrawVoteCommand(Guid PollId, Guid OptionId);