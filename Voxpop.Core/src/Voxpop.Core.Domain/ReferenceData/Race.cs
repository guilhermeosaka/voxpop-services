using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class Race(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Race Create(string code) => new(Guid.NewGuid(), code);
}