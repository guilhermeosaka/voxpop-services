using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Domain.Interfaces;

public interface IUserProfileRepository
{
    Task<UserProfile?> FindByUserIdAsync(Guid userId);
    Task AddAsync(UserProfile user);
}