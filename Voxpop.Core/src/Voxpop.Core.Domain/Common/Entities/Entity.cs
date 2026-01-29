namespace Voxpop.Core.Domain.Common.Entities;

public abstract class Entity(Guid id)
{
    public Guid Id { get; private set; } = id;
}