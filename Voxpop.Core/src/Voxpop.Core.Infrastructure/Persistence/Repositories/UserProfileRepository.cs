using Microsoft.EntityFrameworkCore;
using Voxpop.Core.Domain.Interfaces;
using Voxpop.Core.Domain.UserProfiles;

namespace Voxpop.Core.Infrastructure.Persistence.Repositories;

public class UserProfileRepository(CoreDbContext dbContext) : IUserProfileRepository
{
    public async Task<UserProfile?> FindByUserIdAsync(Guid userId) =>
        await dbContext.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);

    public async Task AddAsync(UserProfile user) => await dbContext.UserProfiles.AddAsync(user);
}