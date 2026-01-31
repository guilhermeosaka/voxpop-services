using Microsoft.Extensions.Options;
using Twilio.Rest.Verify.V2.Service;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Infrastructure.Options;

namespace Voxpop.Identity.Infrastructure.Services.ChannelServices;

public class SmsCodeService(IOptions<TwilioOptions> twilioOptions)
    : ICodeService
{
    private const string Channel = "sms";
    private const string ApprovedStatus = "approved";
    private readonly TwilioOptions _twilioOptions = twilioOptions.Value;

    public async Task SendAsync(string phoneNumber, CancellationToken ct = default)
    {
        _ = await VerificationResource.CreateAsync(
            to: phoneNumber,
            channel: Channel,
            pathServiceSid: _twilioOptions.ServiceSid);
    }

    public async Task<bool> VerifyAsync(string target, string code, CancellationToken ct)
    {
        var verificationCheck = await VerificationCheckResource.CreateAsync(
            to: target,
            code: code,
            pathServiceSid: _twilioOptions.ServiceSid);

        return verificationCheck.Status == ApprovedStatus;
    }
}