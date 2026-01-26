using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Gender(Guid id, string code) : BaseCodeModel(id, code)
{
    public static Gender Create(string code) => new(Guid.NewGuid(), code);
}