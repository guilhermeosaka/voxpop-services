using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Country(Guid id, string code, string continent) : CodeBaseModel(id, code)
{
    public string Continent { get; private set; } = continent;
}