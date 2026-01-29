using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class State(Guid id, string code, Guid countryId) : ReferenceEntity(id, code)
{
    public Guid CountryId { get; private set; } = countryId;
    
    public static State Create(string code, Guid countryId) => new(Guid.NewGuid(), code, countryId);
}