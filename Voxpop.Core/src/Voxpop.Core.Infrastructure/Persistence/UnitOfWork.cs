using Voxpop.Core.Application.Interfaces;

namespace Voxpop.Core.Infrastructure.Persistence;

public class UnitOfWork(CoreDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default) => await dbContext.SaveChangesAsync(ct);
}