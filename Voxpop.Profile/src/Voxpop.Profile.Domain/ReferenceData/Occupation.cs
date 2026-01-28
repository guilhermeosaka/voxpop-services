using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData;

public class Occupation(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Occupation Create(string code) => new(Guid.NewGuid(), code);
}