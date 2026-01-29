using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class Continent(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Continent Create(string code) => new(Guid.NewGuid(), code);
}