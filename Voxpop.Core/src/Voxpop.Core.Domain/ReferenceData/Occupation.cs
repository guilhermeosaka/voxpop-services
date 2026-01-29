using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class Occupation(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Occupation Create(string code) => new(Guid.NewGuid(), code);
}