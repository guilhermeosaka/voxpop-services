using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData;

public class EducationLevel(Guid id, string code) : ReferenceEntity(id, code)
{
    public static EducationLevel Create(string code) => new(Guid.NewGuid(), code);
}