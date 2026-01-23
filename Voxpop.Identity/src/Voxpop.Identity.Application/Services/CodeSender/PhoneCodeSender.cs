using Microsoft.Extensions.Options;
using Voxpop.Contracts.Events;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;

namespace Voxpop.Identity.Application.Services.CodeSender;

public class PhoneCodeSender(
    IMessagePublisher publisher, 
    IOptions<VerificationCodeOptions> verificationCodeOptions) : ICodeSender
{
    public async Task SendAsync(string phoneNumber, string code)
    {
        var message = string.Format(verificationCodeOptions.Value.SmsMessage, code);
        await publisher.PublishAsync(new SmsSend(phoneNumber, message));
    }
}