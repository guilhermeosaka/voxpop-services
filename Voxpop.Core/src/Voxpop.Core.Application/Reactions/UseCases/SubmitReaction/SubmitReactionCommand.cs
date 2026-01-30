using Voxpop.Core.Domain.Votes.Enums;

namespace Voxpop.Core.Application.Reactions.UseCases.SubmitReaction;

public record SubmitReactionCommand(Guid PollId, ReactionType ReactionType);