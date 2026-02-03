using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Common;

public static class Errors
{
    public const string UserNotFoundCode = "User.NotFound";
    public static Error UserNotFound() => new(UserNotFoundCode, "User not found", "User not found.");
    
    public const string VoteNotFoundCode = "Vote.NotFound";
    public static Error VoteNotFound() => new(VoteNotFoundCode, "Vote not found", "Vote not found.");
    
    public const string PollVotingIsClosedCode = "Poll.VotingIsClosed";
    public static Error PollVotingIsClosed() => new(PollVotingIsClosedCode, "Poll voting is closed", "Poll voting is closed.");
    
    public const string PollNotFoundCode = "Poll.NotFound";
    public static Error PollNotFound() => new(PollNotFoundCode, "Poll not found", "Poll not found.");
    
    public const string ReactionNotFoundCode = "Reaction.NotFound";
    public static Error ReactionNotFound() => new(ReactionNotFoundCode, "Reaction not found", "Reaction not found.");
    
    public const string UnauthorizedUserCode = "User.Unauthorized";
    public static Error UserUnauthorized() => new(UnauthorizedUserCode, "User unauthorized", "User is not authorized.");
}