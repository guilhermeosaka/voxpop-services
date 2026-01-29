using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities;

public class Race(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Race Create(string code) => new(Guid.NewGuid(), code);
}