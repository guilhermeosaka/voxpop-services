using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Religion(Guid id, string code) : BaseCodeModel(id, code)
{
    public static Religion Create(string code) => new(Guid.NewGuid(), code);
}