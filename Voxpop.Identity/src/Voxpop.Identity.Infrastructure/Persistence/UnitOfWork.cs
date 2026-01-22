using Voxpop.Identity.Application.Interfaces;

namespace Voxpop.Identity.Infrastructure.Persistence;

public class UnitOfWork(IdentityDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default) => await dbContext.SaveChangesAsync(ct);
}