using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Location(Guid id, Guid countryId, Guid stateId, Guid cityId) : BaseModel(id)
{
    public Guid CountryId { get; private set; } = countryId;
    public Guid StateId { get; private set; } = stateId;
    public Guid CityId { get; private set; } = cityId;
}