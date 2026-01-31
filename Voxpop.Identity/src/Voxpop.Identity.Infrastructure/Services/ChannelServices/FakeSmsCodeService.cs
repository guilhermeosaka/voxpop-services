using Microsoft.Extensions.Logging;
using Voxpop.Identity.Application.Interfaces;

namespace Voxpop.Identity.Infrastructure.Services.ChannelServices;

public class FakeSmsCodeService(ILogger<FakeSmsCodeService> logger) : ICodeService
{
    private const string Code = "123456";
    
    public Task SendAsync(string target, CancellationToken ct = default)
    {
        logger.LogInformation("Fake SMS sent to {Target} with Code: {Code}", target, Code);
        return Task.CompletedTask;
    }

    public Task<bool> VerifyAsync(string target, string code, CancellationToken ct = default)
    {
        return Task.FromResult(code == Code);
    }
}