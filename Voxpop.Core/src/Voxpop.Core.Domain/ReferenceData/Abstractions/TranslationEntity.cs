using Voxpop.Core.Domain.Common.Entities;

namespace Voxpop.Core.Domain.ReferenceData.Abstractions;

public class TranslationEntity(Guid id, string language, string name) : Entity(id)
{
    public string Language { get; private set; } = language;
    public string Name { get; private set; } = name;
    
    public void UpdateName(string newName) => Name = newName;
}