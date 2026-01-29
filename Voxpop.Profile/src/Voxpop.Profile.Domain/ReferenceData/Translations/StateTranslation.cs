using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.ReferenceData.Translations;

public class StateTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static StateTranslation Create(Guid id, string language, string name) => new(id, language, name);
}