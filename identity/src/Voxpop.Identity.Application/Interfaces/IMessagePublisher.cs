namespace Voxpop.Identity.Application.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, string? eventName = null);
}