using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Voxpop.Contracts.Events;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Infrastructure.Services.ChannelServices;

public class EmailCodeService(
    IVerificationCodeRepository verificationCodeRepository,
    IUnitOfWork unitOfWork,
    IMessagePublisher publisher,
    IHasher hasher,
    IOptions<VerificationCodeOptions> verificationCodeOptions) : ICodeService
{
    public async Task SendAsync(string emailAddress, CancellationToken ct = default)
    {
        var verificationCode =
            await verificationCodeRepository.GetByTargetAsync(emailAddress, VerificationCodeChannel.Email);

        if (verificationCode == null)
        {
            verificationCode = VerificationCode.Create(emailAddress, VerificationCodeChannel.Email);
            await verificationCodeRepository.AddAsync(verificationCode);
        }

        var code = GenerateRandomCode();
        var hashed = hasher.Hash(code);

        verificationCode.RefreshCode(hashed, verificationCodeOptions.Value.ExpiresIn);

        await unitOfWork.SaveChangesAsync(ct);

        await publisher.PublishAsync(new EmailCodeSend { EmailAddress = emailAddress, Code = code });

        throw new NotImplementedException($"Email sending is not implemented yet. (Target: {emailAddress}).");
    }

    public async Task<bool> VerifyAsync(string emailAddress, string code, CancellationToken ct)
    {
        var verificationCode =
            await verificationCodeRepository.GetByTargetAsync(emailAddress, VerificationCodeChannel.Email);

        if (verificationCode == null ||
            verificationCode.IsExpired ||
            verificationCode.IsConsumed ||
            !hasher.Verify(verificationCode.CodeHash, code))
            return false;

        verificationCode.Consume();

        await unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    private static string GenerateRandomCode() => RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
}