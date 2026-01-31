namespace Voxpop.Contracts.Events;

public class EmailCodeSend
{
    public string EmailAddress { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
}