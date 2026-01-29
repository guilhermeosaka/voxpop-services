using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class City(Guid id, string code, Guid stateId) : ReferenceEntity(id, code)
{
    public Guid StateId { get; private set; } = stateId;
    
    public static City Create(string code, Guid stateId) => new(Guid.NewGuid(), code, stateId);
}