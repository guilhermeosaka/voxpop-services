using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Domain.Reactions.Repositories;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Reactions.UseCases.WithdrawReaction;

public class WithdrawReactionHandler(
    IReactionRepository repository,
    IRequestContext requestContext,
    IUnitOfWork unitOfWork)
    : IHandler<WithdrawReactionCommand>
{
    public async Task<Result> Handle(WithdrawReactionCommand request, CancellationToken ct)
    {
        var reaction = await repository.FindAsync(requestContext.UserId, request.PollId, request.ReactionType);

        if (reaction == null)
            return Errors.ReactionNotFound();

        repository.Remove(reaction);
        await unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}