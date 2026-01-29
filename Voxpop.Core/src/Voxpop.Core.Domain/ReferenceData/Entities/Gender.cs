using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class Gender(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Gender Create(string code) => new(Guid.NewGuid(), code);
}