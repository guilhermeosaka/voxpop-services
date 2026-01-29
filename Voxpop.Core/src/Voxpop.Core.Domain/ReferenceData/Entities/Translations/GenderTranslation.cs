using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Domain.ReferenceData.Entities.Translations;

public class GenderTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static GenderTranslation Create(Guid id, string language, string name) => new(id, language, name);
}