using Microsoft.EntityFrameworkCore;
using Voxpop.Core.Domain.Votes.Entities;
using Voxpop.Core.Domain.Votes.Repositories;
using Voxpop.Core.Infrastructure.Persistence.Common;
using Voxpop.Core.Infrastructure.Persistence.Common.Repositories;

namespace Voxpop.Core.Infrastructure.Persistence.Votes.Repositories;

public class VoteRepository(CoreDbContext dbContext) : Repository<Vote>(dbContext), IVoteRepository
{
    private readonly CoreDbContext _dbContext = dbContext;

    public async Task<Vote?> FindAsync(Guid userId, Guid pollId) =>
        await _dbContext.Votes.Where(v => v.UserId == userId && v.PollId == pollId).FirstOrDefaultAsync();

    public async Task<Vote?> FindAsync(Guid userId, Guid pollId, Guid optionId) => await _dbContext.Votes
        .Where(v => v.UserId == userId && v.PollId == pollId && v.OptionId == optionId).FirstOrDefaultAsync();
}