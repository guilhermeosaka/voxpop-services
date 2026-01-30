using Microsoft.EntityFrameworkCore;
using Voxpop.Core.Domain.Reactions.Entities;
using Voxpop.Core.Domain.Reactions.Repositories;
using Voxpop.Core.Domain.Votes.Enums;
using Voxpop.Core.Infrastructure.Persistence.Common;
using Voxpop.Core.Infrastructure.Persistence.Common.Repositories;

namespace Voxpop.Core.Infrastructure.Persistence.Reactions.Repositories;

public class ReactionRepository(CoreDbContext dbContext) : Repository<Reaction>(dbContext), IReactionRepository
{
    private readonly CoreDbContext _dbContext = dbContext;

    public async Task<Reaction?> FindAsync(Guid userId, Guid pollId) =>
        await _dbContext.Reactions.Where(v => v.UserId == userId && v.PollId == pollId).FirstOrDefaultAsync();

    public async Task<Reaction?> FindAsync(Guid userId, Guid pollId, ReactionType reactionType) =>
        await _dbContext.Reactions
            .Where(v => v.UserId == userId && v.PollId == pollId && v.ReactionType == reactionType)
            .FirstOrDefaultAsync();
}