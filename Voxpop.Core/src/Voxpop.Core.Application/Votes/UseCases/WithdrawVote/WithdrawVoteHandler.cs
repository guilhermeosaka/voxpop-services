using Voxpop.Core.Application.Common;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Domain.Votes.Repositories;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Votes.UseCases.WithdrawVote;

public class WithdrawVoteHandler(IVoteRepository repository, IRequestContext requestContext, IUnitOfWork unitOfWork)
    : IHandler<WithdrawVoteCommand>
{
    public async Task<Result> Handle(WithdrawVoteCommand request, CancellationToken ct)
    {
        if (!requestContext.UserId.HasValue)
            return Errors.UserUnauthorized();
        
        var vote = await repository.FindAsync(requestContext.UserId.Value, request.PollId, request.OptionId);

        if (vote == null)
            return Errors.VoteNotFound();
        
        repository.Remove(vote);
        await unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}