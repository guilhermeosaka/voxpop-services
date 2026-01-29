using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData.Translations;

public class ContinentTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static ContinentTranslation Create(Guid id, string language, string name) => new(id, language, name);
}