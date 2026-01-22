using Microsoft.EntityFrameworkCore;
using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Infrastructure.Persistence.Repositories;

public class VerificationCodeRepository(IdentityDbContext dbContext) : IVerificationCodeRepository
{
    public async Task<VerificationCode?> GetByPhoneNumber(string phoneNumber) =>
        await dbContext.VerificationCodes.FirstOrDefaultAsync(vc =>
            vc.Target == phoneNumber && vc.Channel == VerificationCodeChannel.PhoneNumber);

    public async Task AddAsync(VerificationCode verificationCode) =>
        await dbContext.VerificationCodes.AddAsync(verificationCode);
}