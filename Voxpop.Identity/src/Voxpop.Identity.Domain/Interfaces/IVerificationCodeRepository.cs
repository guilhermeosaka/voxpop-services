using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Domain.Interfaces;

public interface IVerificationCodeRepository
{
    Task<VerificationCode?> GetByTargetAsync(string target, VerificationCodeChannel channel);
    Task AddAsync(VerificationCode verificationCode);
}