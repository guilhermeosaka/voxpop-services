using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Location(Guid id, Guid cityId) : BaseModel(id)
{
    public Guid CityId { get; private set; } = cityId;
}