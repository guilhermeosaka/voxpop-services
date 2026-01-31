using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Debugging;

namespace Voxpop.Notification.Infrastructure.Extensions;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder UseLogger(this ILoggingBuilder builder, IConfiguration configuration)
    {
        SelfLog.Enable(msg => { Console.Error.WriteLine(msg); });

        builder.ClearProviders();
        builder.AddSerilog(new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .CreateLogger());

        return builder;
    }
}