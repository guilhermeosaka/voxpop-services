namespace Voxpop.Core.Application.Votes.UseCases.SubmitVote.Strategies;

public interface ISubmitVoteStrategy
{
    Task SubmitVote(SubmitVoteCommand command, CancellationToken ct = default);
}