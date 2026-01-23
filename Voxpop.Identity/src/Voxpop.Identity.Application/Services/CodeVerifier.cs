using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Interfaces;

namespace Voxpop.Identity.Application.Services;

public class CodeVerifier(
    IVerificationCodeRepository verificationCodeRepository,
    IUnitOfWork unitOfWork,
    IHasher hasher)
{
    public async Task<bool> VerifyAsync(string target, VerificationCodeChannel channel, string code, CancellationToken ct)
    {
        var verificationCode = await verificationCodeRepository.GetByTargetAsync(target, channel);

        if (verificationCode == null ||
            verificationCode.IsExpired ||
            verificationCode.IsConsumed ||
            !hasher.Verify(verificationCode.CodeHash, code))
            return false;
        
        verificationCode.Consume();

        await unitOfWork.SaveChangesAsync(ct);
        
        return true;
    }
}