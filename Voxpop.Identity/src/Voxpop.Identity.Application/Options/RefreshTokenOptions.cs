namespace Voxpop.Identity.Application.Options;

public class RefreshTokenOptions
{
    public required TimeSpan ExpiresIn { get; init; }
}