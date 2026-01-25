namespace Voxpop.Profile.Domain.Abstractions;

public abstract class BaseModel(Guid id)
{
    public Guid Id { get; private set; } = id;
}