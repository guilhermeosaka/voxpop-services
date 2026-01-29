using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData.Translations;

public class CityTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static CityTranslation Create(Guid id, string language, string name) => new(id, language, name);
}