using Microsoft.Extensions.Options;
using Voxpop.Contracts.Events;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Application.Services;

public class VerificationCodeService(
    IUserRepository<User> userRepository,
    IMessagePublisher publisher,
    IOptions<VerificationCodeOptions> verificationCodeOptions)
{
    public async Task SendVerificationCodeAsync(string phoneNumber)
    {
        var user = await userRepository.FindByPhoneNumberAsync(phoneNumber);

        if (user == null)
            return;
        
        user.GenerateVerificationCode(verificationCodeOptions.Value.ExpiresIn);

        await userRepository.UpdateAsync(user);

        var message = string.Format(verificationCodeOptions.Value.Message, user.VerificationCode);
        await publisher.PublishAsync(new SmsSend(phoneNumber, message));
    }
}