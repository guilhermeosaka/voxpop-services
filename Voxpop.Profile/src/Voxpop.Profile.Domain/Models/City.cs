using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class City(Guid id, string code, Guid stateId) : CodeBaseModel(id, code)
{
    public Guid StateId { get; private set; } = stateId;
}