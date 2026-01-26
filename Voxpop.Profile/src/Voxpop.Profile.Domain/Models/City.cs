using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class City(Guid id, string code, string name, Guid stateId) : BaseCodeModel(id, code)
{
    public string Name { get; private set; } = name;
    public Guid StateId { get; private set; } = stateId;
    
    public static City Create(string code, string name, Guid stateId) => new(Guid.NewGuid(), code, name, stateId);
}