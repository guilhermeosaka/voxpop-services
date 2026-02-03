using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Votes.UseCases.SubmitVote.Strategies;

public interface ISubmitVoteStrategy
{
    Task<Result> SubmitVote(SubmitVoteCommand command, CancellationToken ct = default);
}