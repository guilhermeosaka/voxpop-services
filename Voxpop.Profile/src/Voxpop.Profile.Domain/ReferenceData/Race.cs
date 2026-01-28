using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData;

public class Race(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Race Create(string code) => new(Guid.NewGuid(), code);
}