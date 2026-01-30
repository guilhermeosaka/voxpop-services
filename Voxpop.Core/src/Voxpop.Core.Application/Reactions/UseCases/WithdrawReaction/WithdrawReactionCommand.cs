using Voxpop.Core.Domain.Votes.Enums;

namespace Voxpop.Core.Application.Reactions.UseCases.WithdrawReaction;

public record WithdrawReactionCommand(Guid PollId, ReactionType ReactionType);