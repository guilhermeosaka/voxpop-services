using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData.Translations;

public class OccupationTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static OccupationTranslation Create(Guid id, string language, string name) => new(id, language, name);
}