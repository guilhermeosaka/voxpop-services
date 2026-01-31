using Voxpop.Notification.Infrastructure.Extensions;
using Voxpop.Notification.Infrastructure.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.UseLogger(builder.Configuration);

builder.Services
    .AddMassTransit(builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!);

var host = builder.Build();
host.Run();
