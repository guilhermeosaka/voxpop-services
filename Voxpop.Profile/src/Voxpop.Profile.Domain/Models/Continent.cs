using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Continent(Guid id, string code) : BaseCodeModel(id, code)
{
    public static Continent Create(string code) => new(Guid.NewGuid(), code);
}