using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Infrastructure.Options;

namespace Voxpop.Identity.Infrastructure.Messaging;

public class RabbitMqPublisher(IConnection connection, IOptions<RabbitMqOptions> options) : IMessagePublisher
{
    public async Task PublishAsync<T>(T message, string? eventName = null)
    {
        await using var channel = await connection.CreateChannelAsync();

        var exchange = options.Value.Exchange;
        var routingKey = eventName ?? options.Value.RoutingKeys[typeof(T).Name];

        await channel.ExchangeDeclareAsync(
            exchange,
            type: ExchangeType.Topic,
            durable: true);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        await channel.BasicPublishAsync(
            exchange,
            routingKey,
            body: body
        );
    }
}