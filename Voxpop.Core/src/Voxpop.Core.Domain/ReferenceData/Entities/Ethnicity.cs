using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class Ethnicity(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Ethnicity Create(string code) => new(Guid.NewGuid(), code);
}