using Voxpop.Core.Domain.Common.Entities;

namespace Voxpop.Core.Domain.ReferenceData.Abstractions;

public abstract class ReferenceEntity(Guid id, string code) : Entity(id)
{
    public string Code { get; private set; } = code;
}