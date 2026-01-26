using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Voxpop.Profile.Infrastructure.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseLogger(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        Serilog.Debugging.SelfLog.Enable(msg =>
        {
            Console.Error.WriteLine(msg);
        });
        
        return hostBuilder.UseSerilog((_, services, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName();
        });
    }
}