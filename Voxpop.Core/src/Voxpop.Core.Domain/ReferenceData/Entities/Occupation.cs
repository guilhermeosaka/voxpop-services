using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class Occupation(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Occupation Create(string code) => new(Guid.NewGuid(), code);
}