using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Identity.Application.Handlers.Verifications.CreateCode;

public class CreateCodeHandler(
    IVerificationCodeRepository verificationCodeRepository,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider,
    IOptions<VerificationCodeOptions> verificationCodeOptions)
    : IHandler<CreateCodeCommand>
{
    public async Task Handle(CreateCodeCommand request, CancellationToken ct)
    {
        var verificationCode = await verificationCodeRepository.GetByTargetAsync(request.Target, request.Channel);

        if (verificationCode == null)
        {
            verificationCode = new VerificationCode(request.Target, request.Channel);
            await verificationCodeRepository.AddAsync(verificationCode);
        }

        verificationCode.RefreshCode(verificationCodeOptions.Value.ExpiresIn);

        await unitOfWork.SaveChangesAsync(ct);

        var codeSender = serviceProvider.GetRequiredKeyedService<ICodeSender>(request.Channel);
        await codeSender.SendAsync(request.Target, verificationCode.Code);
    }
}