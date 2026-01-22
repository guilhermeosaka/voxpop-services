using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Domain.Interfaces;

public interface IVerificationCodeRepository
{
    Task<VerificationCode?> GetByPhoneNumber(string phoneNumber);
    Task AddAsync(VerificationCode verificationCode);
}