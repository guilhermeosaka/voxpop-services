namespace Voxpop.Identity.Infrastructure.Options;

public class RabbitMqOptions
{
    public required string HostName { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
    public required  string Exchange { get; init; }
    public Dictionary<string, string> RoutingKeys { get; init; } = new();
    public string VirtualHost { get; init; } = "/";
    public int Port { get; init; } = 5672;
}