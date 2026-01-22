namespace Voxpop.Identity.Application.Options;

public class VerificationCodeOptions
{
    public TimeSpan ExpiresIn { get; init; }
    public required string Message { get; init; }
}