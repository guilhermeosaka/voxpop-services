using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class Religion(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Religion Create(string code) => new(Guid.NewGuid(), code);
}