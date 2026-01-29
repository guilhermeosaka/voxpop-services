using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class Continent(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Continent Create(string code) => new(Guid.NewGuid(), code);
}