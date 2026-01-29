using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class State(Guid id, string code, Guid countryId) : ReferenceEntity(id, code)
{
    public Guid CountryId { get; private set; } = countryId;
    
    public static State Create(string code, Guid countryId) => new(Guid.NewGuid(), code, countryId);
}