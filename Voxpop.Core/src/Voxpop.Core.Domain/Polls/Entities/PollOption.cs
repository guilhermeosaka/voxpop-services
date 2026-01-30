using Voxpop.Core.Domain.Common.Entities;

namespace Voxpop.Core.Domain.Polls.Entities;

public class PollOption(Guid id, Guid pollId, int order, string value) : Entity(id)
{
    public Guid PollId { get; init; } = pollId;
    public int Order { get; set; } = order;
    public string Value { get; init; } = value;
    public static PollOption Create(Guid pollId, int order, string value) => new(Guid.NewGuid(), pollId, order, value);
}