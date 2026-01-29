using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData.Translations;

public class EducationLevelTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static EducationLevelTranslation Create(Guid id, string language, string name) => new(id, language, name);
}