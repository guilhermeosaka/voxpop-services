using Voxpop.Core.Domain.Common.Entities;
using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.Votes.Enums;

namespace Voxpop.Core.Domain.Reactions.Entities;

public class Reaction(Guid id, Guid userId, Guid pollId, ReactionType reactionType)
    : Entity(id), IAuditable, IAggregateRoot
{
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }

    public Guid UserId { get; set; } = userId;
    public Guid PollId { get; set; } = pollId;
    public ReactionType ReactionType { get; set; } = reactionType;

    public static Reaction Create(Guid userId, Guid pollId, ReactionType reactionType) =>
        new(Guid.NewGuid(), userId, pollId, reactionType);
    
    public void Update(ReactionType reactionType) => ReactionType = reactionType;
}