using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Domain.Polls;

public class PollOption(Guid id, Guid pollId, string value) : Entity(id)
{
    public Guid PollId { get; init; } = pollId;
    public string Value { get; init; } = value;
    public static PollOption Create(Guid pollId, string value) => new(Guid.NewGuid(), pollId, value);
}