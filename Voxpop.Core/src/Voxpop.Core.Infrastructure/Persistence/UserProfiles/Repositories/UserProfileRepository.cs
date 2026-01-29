using Microsoft.EntityFrameworkCore;
using Voxpop.Core.Domain.UserProfiles.Entities;
using Voxpop.Core.Domain.UserProfiles.Repositories;
using Voxpop.Core.Infrastructure.Persistence.Common;
using Voxpop.Core.Infrastructure.Persistence.Common.Repositories;

namespace Voxpop.Core.Infrastructure.Persistence.UserProfiles.Repositories;

public class UserProfileRepository(CoreDbContext dbContext) : Repository<UserProfile>(dbContext), IUserProfileRepository
{
    private readonly CoreDbContext _dbContext = dbContext;

    public async Task<UserProfile?> FindByUserIdAsync(Guid userId) =>
        await _dbContext.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);
}