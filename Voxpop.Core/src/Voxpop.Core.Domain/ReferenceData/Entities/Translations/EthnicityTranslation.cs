using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities.Translations;

public class EthnicityTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static EthnicityTranslation Create(Guid id, string language, string name) => new(id, language, name);
}