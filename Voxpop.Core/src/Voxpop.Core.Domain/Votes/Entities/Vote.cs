using Voxpop.Core.Domain.Common.Entities;
using Voxpop.Core.Domain.Common.Interfaces;

namespace Voxpop.Core.Domain.Votes.Entities;

public class Vote(Guid id, Guid userId, Guid pollId, Guid optionId) : Entity(id), IAuditable, IAggregateRoot
{
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    
    public Guid UserId { get; private set; } = userId;
    public Guid PollId { get; private set; } = pollId;
    public Guid OptionId { get; private set; } = optionId;

    public static Vote Create(Guid userId, Guid pollId, Guid optionId) => new(Guid.NewGuid(), userId, pollId, optionId);
    
    public void Update(Guid optionId) => OptionId = optionId;
}