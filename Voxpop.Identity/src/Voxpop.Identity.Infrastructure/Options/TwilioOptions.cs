namespace Voxpop.Identity.Infrastructure.Options;

public class TwilioOptions
{
    public required string AccountSid { get; init; }
    public required string AuthToken { get; init; }
    public required string ServiceSid { get; init; }
}