using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData;

public class City(Guid id, string code, Guid stateId) : ReferenceEntity(id, code)
{
    public Guid StateId { get; private set; } = stateId;
    
    public static City Create(string code, Guid stateId) => new(Guid.NewGuid(), code, stateId);
}