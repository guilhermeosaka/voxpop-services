using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.ReferenceData.Translations;

public class GenderTranslation(Guid id, string language, string name) : TranslationEntity(id, language, name)
{
    public static GenderTranslation Create(Guid id, string language, string name) => new(id, language, name);
}