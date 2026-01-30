using Microsoft.Extensions.DependencyInjection;
using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Polls.Queries;
using Voxpop.Core.Application.Votes.UseCases.SubmitVote.Strategies;
using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Votes.UseCases.SubmitVote;

public class SubmitVoteHandler(
    IServiceProvider serviceProvider,
    IPollQueries pollQueries,
    IClock clock)
    : IHandler<SubmitVoteCommand>
{
    public async Task<Result> Handle(SubmitVoteCommand request, CancellationToken ct)
    {
        var votingInfo = await pollQueries.FindVotingInfoAsync(request.PollId);

        if (votingInfo == null)
            return Errors.PollNotFound();
        
        if (votingInfo.IsClosed && votingInfo.ExpiresAt > clock.UtcNow)
            return Errors.PollVotingIsClosed();

        var strategy = serviceProvider.GetRequiredKeyedService<ISubmitVoteStrategy>(votingInfo.VoteMode);
        await strategy.SubmitVote(request, ct);

        return Result.Success();
    }
}