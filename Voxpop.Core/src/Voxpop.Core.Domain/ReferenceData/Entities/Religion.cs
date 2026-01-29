using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class Religion(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Religion Create(string code) => new(Guid.NewGuid(), code);
}