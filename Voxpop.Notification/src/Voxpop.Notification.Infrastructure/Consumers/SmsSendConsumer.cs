using MassTransit;
using Microsoft.Extensions.Logging;
using Voxpop.Contracts.Events;

namespace Voxpop.Notification.Infrastructure.Consumers;

public class SmsSendConsumer(ILogger<SmsSendConsumer> logger) : IConsumer<SmsSend>
{
    public async Task Consume(ConsumeContext<SmsSend> context)
    {
        var message = context.Message;

        logger.LogInformation(
            "SMS sent to {PhoneNumber} with message: {Message}", 
            message.PhoneNumber,
            message.Message);

        await Task.CompletedTask;
    }
}