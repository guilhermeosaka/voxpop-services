using Microsoft.Extensions.Options;
using Voxpop.Contracts.Events;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Identity.Application.Handlers.Verifications.CreatePhoneCode;

public class CreatePhoneCodeHandler(
    IVerificationCodeRepository verificationCodeRepository,
    IMessagePublisher publisher,
    IUnitOfWork unitOfWork,
    IOptions<VerificationCodeOptions> verificationCodeOptions)
    : IHandler<CreatePhoneCodeCommand>
{
    public async Task Handle(CreatePhoneCodeCommand request, CancellationToken ct)
    {
        var verificationCode = await verificationCodeRepository.GetByPhoneNumber(request.PhoneNumber);

        if (verificationCode == null)
        {
            verificationCode = new VerificationCode(request.PhoneNumber, VerificationCodeChannel.PhoneNumber);
            await verificationCodeRepository.AddAsync(verificationCode);
        }

        verificationCode.RefreshCode(verificationCodeOptions.Value.ExpiresIn);

        await unitOfWork.SaveChangesAsync(ct);
        
        var message = string.Format(verificationCodeOptions.Value.Message, verificationCode.Code);
        await publisher.PublishAsync(new SmsSend(request.PhoneNumber, message));
    }
}