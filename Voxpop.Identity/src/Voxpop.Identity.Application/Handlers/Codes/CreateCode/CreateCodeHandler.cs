using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Packages.Handler.Interfaces;
using Voxpop.Packages.Handler.Types;

namespace Voxpop.Identity.Application.Handlers.Codes.CreateCode;

public class CreateCodeHandler(
    IVerificationCodeRepository verificationCodeRepository,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider,
    IHasher hasher,
    IOptions<VerificationCodeOptions> verificationCodeOptions)
    : IHandler<CreateCodeCommand>
{
    public async Task<Result> Handle(CreateCodeCommand request, CancellationToken ct)
    {
        var verificationCode = await verificationCodeRepository.GetByTargetAsync(request.Target, request.Channel);

        if (verificationCode == null)
        {
            verificationCode = VerificationCode.Create(request.Target, request.Channel);
            await verificationCodeRepository.AddAsync(verificationCode);
        }

        var code = GenerateRandomCode();
        var hashed = hasher.Hash(code);
        
        verificationCode.RefreshCode(hashed, verificationCodeOptions.Value.ExpiresIn);

        await unitOfWork.SaveChangesAsync(ct);

        var codeSender = serviceProvider.GetRequiredKeyedService<ICodeSender>(request.Channel);
        await codeSender.SendAsync(request.Target, code);

        return Result.Success();
    }

    private static string GenerateRandomCode() => RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
}