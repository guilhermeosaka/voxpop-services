using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Domain.Votes.Entities;
using Voxpop.Core.Domain.Votes.Repositories;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Votes.UseCases.SubmitVote.Strategies;

public class MultipleChoiceStrategy(
    IVoteRepository repository,
    IRequestContext requestContext,
    IUnitOfWork unitOfWork) : ISubmitVoteStrategy
{
    public async Task<Result> SubmitVote(SubmitVoteCommand request, CancellationToken ct = default)
    {
        if (!requestContext.UserId.HasValue)
            return Errors.UserUnauthorized();
        
        var userId = requestContext.UserId.Value;
        
        var vote = await repository.FindAsync(userId, request.PollId, request.OptionId);

        if (vote == null)
            await repository.AddAsync(Vote.Create(userId, request.PollId, request.OptionId, request.OptionId));

        await unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}