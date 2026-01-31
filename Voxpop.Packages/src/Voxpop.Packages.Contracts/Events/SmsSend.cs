namespace Voxpop.Contracts.Events;

public class SmsSend
{
    public string PhoneNumber { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}