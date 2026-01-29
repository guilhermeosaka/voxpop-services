using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData;

public class Ethnicity(Guid id, string code) : ReferenceEntity(id, code)
{
    public static Ethnicity Create(string code) => new(Guid.NewGuid(), code);
}