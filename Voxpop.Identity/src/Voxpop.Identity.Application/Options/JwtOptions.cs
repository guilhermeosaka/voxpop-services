namespace Voxpop.Identity.Application.Options;

public class JwtOptions
{
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string[] Audiences { get; init; }
    public required TimeSpan ExpiresIn { get; init; }
}