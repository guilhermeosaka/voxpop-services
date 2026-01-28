using Microsoft.EntityFrameworkCore;
using Voxpop.Profile.Domain.Interfaces;
using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Infrastructure.Persistence.Repositories;

public class ProfileRepository(ProfileDbContext dbContext) : IProfileRepository
{
    public async Task<UserProfile?> FindByUserIdAsync(Guid userId) =>
        await dbContext.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);

    public async Task AddAsync(UserProfile user) => await dbContext.UserProfiles.AddAsync(user);
}