using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData.Translations;

public class EthnicityTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static EthnicityTranslation Create(Guid id, string language, string name) => new(id, language, name);
}