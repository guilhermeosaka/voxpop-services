using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.Reactions.Entities;
using Voxpop.Core.Domain.Votes.Enums;

namespace Voxpop.Core.Domain.Reactions.Repositories;

public interface IReactionRepository : IRepository<Reaction>
{
    Task<Reaction?> FindAsync(Guid userId, Guid pollId);
    Task<Reaction?> FindAsync(Guid userId, Guid pollId, ReactionType reactionType);
}