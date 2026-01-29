using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Common;

public static class Errors
{
    private const string UserNotFoundCode = "User.NotFound";

    public static Error UserNotFound() => new(UserNotFoundCode, "User not found", "User not found.");
}