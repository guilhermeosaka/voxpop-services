using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class Country(Guid id, string code, Guid continentId) : ReferenceEntity(id, code)
{
    public Guid ContinentId { get; private set; } = continentId;

    public static Country Create(string code, Guid continentId) => new(Guid.NewGuid(), code, continentId);
}