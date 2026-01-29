using Voxpop.Core.Domain.Common.Entities;

namespace Voxpop.Core.Domain.Polls.Entities;

public class PollOption(Guid id, Guid pollId, string value) : Entity(id)
{
    public Guid PollId { get; init; } = pollId;
    public string Value { get; init; } = value;
    public static PollOption Create(Guid pollId, string value) => new(Guid.NewGuid(), pollId, value);
}