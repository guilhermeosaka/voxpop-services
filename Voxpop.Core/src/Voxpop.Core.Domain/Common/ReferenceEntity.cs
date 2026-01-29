namespace Voxpop.Core.Domain.Common;

public abstract class ReferenceEntity(Guid id, string code) : Entity(id)
{
    public string Code { get; private set; } = code;
}