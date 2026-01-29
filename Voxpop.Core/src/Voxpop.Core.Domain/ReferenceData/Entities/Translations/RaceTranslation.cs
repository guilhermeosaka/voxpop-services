using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities.Translations;

public class RaceTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static RaceTranslation Create(Guid id, string language, string name) => new(id, language, name);
}