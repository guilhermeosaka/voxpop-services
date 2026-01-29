using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.UserProfiles.Entities;

namespace Voxpop.Core.Domain.UserProfiles.Repositories;

public interface IUserProfileRepository : IRepository<UserProfile>
{
    Task<UserProfile?> FindByUserIdAsync(Guid userId);
}