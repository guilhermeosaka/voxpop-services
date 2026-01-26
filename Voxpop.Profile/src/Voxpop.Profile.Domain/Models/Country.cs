using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Country(Guid id, string code, Guid continentId) : BaseCodeModel(id, code)
{
    public Guid ContinentId { get; private set; } = continentId;

    public static Country Create(string code, Guid continentId) => new(Guid.NewGuid(), code, continentId);
}