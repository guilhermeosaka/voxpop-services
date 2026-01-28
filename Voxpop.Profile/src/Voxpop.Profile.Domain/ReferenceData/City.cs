using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData;

public class City(Guid id, string code, string name, Guid stateId) : ReferenceEntity(id, code)
{
    public string Name { get; private set; } = name;
    public Guid StateId { get; private set; } = stateId;
    
    public static City Create(string code, string name, Guid stateId) => new(Guid.NewGuid(), code, name, stateId);
}