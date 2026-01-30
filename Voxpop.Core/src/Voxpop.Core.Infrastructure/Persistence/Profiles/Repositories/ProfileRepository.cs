using Microsoft.EntityFrameworkCore;
using Voxpop.Core.Domain.Profiles.Entities;
using Voxpop.Core.Domain.Profiles.Repositories;
using Voxpop.Core.Infrastructure.Persistence.Common;
using Voxpop.Core.Infrastructure.Persistence.Common.Repositories;

namespace Voxpop.Core.Infrastructure.Persistence.Profiles.Repositories;

public class ProfileRepository(CoreDbContext dbContext) : Repository<Profile>(dbContext), IProfileRepository
{
    private readonly CoreDbContext _dbContext = dbContext;

    public async Task<Profile?> FindByUserIdAsync(Guid userId) =>
        await _dbContext.Profiles.FirstOrDefaultAsync(up => up.UserId == userId);
}