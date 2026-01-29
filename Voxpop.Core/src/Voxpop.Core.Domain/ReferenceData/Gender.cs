using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class Gender(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Gender Create(string code) => new(Guid.NewGuid(), code);
}