using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.Votes.Entities;

namespace Voxpop.Core.Domain.Votes.Repositories;

public interface IVoteRepository : IRepository<Vote>
{
    Task<Vote?> FindAsync(Guid userId, Guid pollId);
    Task<Vote?> FindAsync(Guid userId, Guid pollId, Guid optionId);
    
}