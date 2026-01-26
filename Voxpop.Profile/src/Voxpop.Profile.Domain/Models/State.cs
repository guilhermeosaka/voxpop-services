using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class State(Guid id, string code, Guid countryId) : BaseCodeModel(id, code)
{
    public Guid CountryId { get; private set; } = countryId;
    
    public static State Create(string code, Guid countryId) => new(Guid.NewGuid(), code, countryId);
}