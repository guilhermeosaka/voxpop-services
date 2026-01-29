namespace Voxpop.Core.Domain.Common.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<T?> FindByIdAsync(Guid id);
    Task AddAsync(T item);
}