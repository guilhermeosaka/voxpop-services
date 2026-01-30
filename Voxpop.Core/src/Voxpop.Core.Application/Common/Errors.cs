using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Common;

public static class Errors
{
    public const string UserNotFoundCode = "User.NotFound";
    public static Error UserNotFound() => new(UserNotFoundCode, "User not found", "User not found.");

    public const string InvalidVoteStateCode = "Vote.InvalidState";
    public static Error InvalidVoteState() => new(InvalidVoteStateCode, "Invalid vote state", "Vote is not in a valid state.");
    
    public const string PollVotingIsClosedCode = "Poll.VotingIsClosed";
    public static Error PollVotingIsClosed() => new(PollVotingIsClosedCode, "Poll voting is closed", "Poll voting is closed.");
}