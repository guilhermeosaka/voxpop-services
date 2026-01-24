using Voxpop.Profile.Application.Interfaces;

namespace Voxpop.Profile.Infrastructure.Persistence;

public class UnitOfWork(ProfileDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default) => await dbContext.SaveChangesAsync(ct);
}