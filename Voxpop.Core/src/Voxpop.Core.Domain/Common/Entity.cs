namespace Voxpop.Core.Domain.Common;

public abstract class Entity(Guid id)
{
    public Guid Id { get; private set; } = id;
}