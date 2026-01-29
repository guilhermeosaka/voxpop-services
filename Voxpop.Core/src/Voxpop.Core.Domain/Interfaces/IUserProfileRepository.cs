using Voxpop.Core.Domain.UserProfiles;

namespace Voxpop.Core.Domain.Interfaces;

public interface IUserProfileRepository
{
    Task<UserProfile?> FindByUserIdAsync(Guid userId);
    Task AddAsync(UserProfile user);
}