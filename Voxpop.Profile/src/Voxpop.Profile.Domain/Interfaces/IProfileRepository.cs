using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Domain.Interfaces;

public interface IProfileRepository
{
    Task<UserProfile?> FindByUserIdAsync(Guid userId);
    Task AddAsync(UserProfile user);
}