using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData;

public class Religion(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Religion Create(string code) => new(Guid.NewGuid(), code);
}