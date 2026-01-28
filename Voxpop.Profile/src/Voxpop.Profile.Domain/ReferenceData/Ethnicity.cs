using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData;

public class Ethnicity(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Ethnicity Create(string code) => new(Guid.NewGuid(), code);
}