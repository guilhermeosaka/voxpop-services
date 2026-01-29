using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class EducationLevel(Guid id, string code) : ReferenceEntity(id, code)
{
    public static EducationLevel Create(string code) => new(Guid.NewGuid(), code);
}