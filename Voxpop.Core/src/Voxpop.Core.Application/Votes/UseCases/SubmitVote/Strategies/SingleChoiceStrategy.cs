using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Domain.Votes.Entities;
using Voxpop.Core.Domain.Votes.Repositories;

namespace Voxpop.Core.Application.Votes.UseCases.SubmitVote.Strategies;

public class SingleChoiceStrategy(
    IVoteRepository repository,
    IRequestContext requestContext,
    IUnitOfWork unitOfWork) : ISubmitVoteStrategy
{
    public async Task SubmitVote(SubmitVoteCommand request, CancellationToken ct = default)
    {
        var userId = requestContext.UserId;
        
        var vote = await repository.FindAsync(userId, request.PollId);

        if (vote == null)
            await repository.AddAsync(Vote.Create(userId, request.PollId, request.OptionId));
        else
            vote.Update(request.OptionId);

        await unitOfWork.SaveChangesAsync(ct);
    }
}