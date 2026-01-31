using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Voxpop.Contracts.Events;
using Voxpop.Notification.Infrastructure.Consumers;
using Voxpop.Notification.Infrastructure.Options;

namespace Voxpop.Notification.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, RabbitMqOptions options)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<SmsSendConsumer>();

            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(options.HostName, options.VirtualHost, h =>
                {
                    h.Username(options.UserName);
                    h.Password(options.Password);
                });

                cfg.ReceiveEndpoint(options.Queues[nameof(SmsSend)], e =>
                {
                    e.ConfigureConsumeTopology = false;

                    e.Bind(options.Exchange, b =>
                    {
                        b.RoutingKey = options.RoutingKeys[nameof(SmsSend)];
                        b.ExchangeType = ExchangeType.Topic;
                    });

                    e.DefaultContentType = new System.Net.Mime.ContentType("application/json");
                    e.UseRawJsonDeserializer();

                    e.ConfigureConsumer<SmsSendConsumer>(context);
                });
            });
        });

        return services;
    }
}