using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Ethnicity(Guid id, string code) : BaseCodeModel(id, code)
{
    public static Ethnicity Create(string code) => new(Guid.NewGuid(), code);
}