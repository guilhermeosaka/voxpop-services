using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class Country(Guid id, string code, Guid continentId) : ReferenceEntity(id, code)
{
    public Guid ContinentId { get; private set; } = continentId;

    public static Country Create(string code, Guid continentId) => new(Guid.NewGuid(), code, continentId);
}