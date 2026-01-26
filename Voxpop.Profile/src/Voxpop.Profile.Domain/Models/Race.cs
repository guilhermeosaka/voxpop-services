using Voxpop.Profile.Domain.Models.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class Race(Guid id, string code) : BaseCodeModel(id, code)
{
    public static Race Create(string code) => new(Guid.NewGuid(), code);
}