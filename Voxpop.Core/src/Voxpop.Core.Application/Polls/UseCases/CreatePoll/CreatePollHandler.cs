using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.Polls.Entities;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Polls.UseCases.CreatePoll;

public class CreatePollHandler(IRepository<Poll> repository, IUnitOfWork unitOfWork) : IHandler<CreatePollCommand>
{
    public async Task<Result> Handle(CreatePollCommand request, CancellationToken ct)
    {
        var poll = Poll.Create(request.Question, request.ExpiresAt, request.VoteMode);
        
        foreach (var option in request.Options)
            poll.AddOption(option.Value);

        await repository.AddAsync(poll);
        await unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}