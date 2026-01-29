using Voxpop.Core.Application.Common.Interfaces;

namespace Voxpop.Core.Infrastructure.Persistence.Common;

public class UnitOfWork(CoreDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default) => await dbContext.SaveChangesAsync(ct);
}