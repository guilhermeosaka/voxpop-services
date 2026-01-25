using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class State(Guid id, string code, Guid countryId) : CodeBaseModel(id, code)
{
    public Guid CountryId { get; private set; } = countryId;
}