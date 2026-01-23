using Voxpop.Identity.Domain.Enums;
using Voxpop.Packages.Handler.Types;

namespace Voxpop.Identity.Application.Common;

public static class Errors
{
    public const string InvalidCodeCode = "Code.Invalid";
    public const string InvalidRefreshTokenCode = "RefreshToken.Invalid";
    public const string UserNotFoundCode = "User.NotFound";
    public const string UserConflictCode = "User.Conflict";
    
    public static Error InvalidCode(string code) => 
        new(InvalidCodeCode, "Invalid code", $"Code '{code}' is invalid.");
    
    public static Error InvalidRefreshToken(string token) => 
        new(InvalidRefreshTokenCode, "Invalid RefreshToken", $"RefreshToken '{token}' is invalid.");

    public static Error UserNotFound(string target, VerificationCodeChannel channel) =>
        new(UserNotFoundCode, "User not found", $"User with {channel} '{target}' not found.");
    
    public static Error UserNotFound(Guid userId) =>
        new(UserNotFoundCode, "User not found", $"User with id '{userId}' not found.");

    public static Error UserConflict(string target, VerificationCodeChannel channel) =>
        new(UserConflictCode, "User already exists", $"User with {channel} '{target}' already exists.");
}