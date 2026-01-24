using Voxpop.Template.Application.Interfaces;

namespace Voxpop.Template.Infrastructure.Persistence;

public class UnitOfWork(TemplateDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default) => await dbContext.SaveChangesAsync(ct);
}