using Voxpop.Core.Domain.Common.Interfaces;

namespace Voxpop.Core.Infrastructure.Persistence.Common.Repositories;

public class Repository<T>(CoreDbContext dbContext) : IRepository<T> where T : class, IAggregateRoot
{
    public async Task<T?> FindByIdAsync(Guid id) => await dbContext.Set<T>().FindAsync(id);
 
    public async Task AddAsync(T item) => await dbContext.Set<T>().AddAsync(item);
    
    public void Remove(T item) => dbContext.Set<T>().Remove(item);
}