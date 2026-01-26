using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Occupation(Guid id, string code) : BaseCodeModel(id, code)
{
    public static Occupation Create(string code) => new(Guid.NewGuid(), code);
}