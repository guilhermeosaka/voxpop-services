using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Application.Polls.Queries;
using Voxpop.Core.Domain.Reactions.Entities;
using Voxpop.Core.Domain.Reactions.Repositories;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Reactions.UseCases.SubmitReaction;

public class SubmitReactionHandler(
    IPollQueries pollQueries,
    IReactionRepository repository,
    IRequestContext requestContext,
    IUnitOfWork unitOfWork
)
    : IHandler<SubmitReactionCommand>
{
    public async Task<Result> Handle(SubmitReactionCommand request, CancellationToken ct)
    {
        if (!requestContext.UserId.HasValue)
            return Errors.UserUnauthorized();

        var userId = requestContext.UserId.Value;
        
        var votingInfo = await pollQueries.FindVotingInfoAsync(request.PollId, ct);

        if (votingInfo == null)
            return Errors.PollNotFound();
        
        var reaction = await repository.FindAsync(userId, request.PollId);
        
        if (reaction == null)
            await repository.AddAsync(Reaction.Create(userId, request.PollId, request.ReactionType));
        else
            reaction.Update(request.ReactionType);

        await unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}