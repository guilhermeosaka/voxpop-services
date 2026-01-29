using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class EducationLevel(Guid id, string code) : ReferenceEntity(id, code)
{
    public static EducationLevel Create(string code) => new(Guid.NewGuid(), code);
}