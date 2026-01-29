using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData.Translations;

public class ContinentTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static ContinentTranslation Create(Guid id, string language, string name) => new(id, language, name);
}