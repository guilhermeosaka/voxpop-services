using Microsoft.EntityFrameworkCore;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(IdentityDbContext dbContext) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenId(string tokenId) =>
        await dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.TokenId == tokenId);

    public async Task AddAsync(RefreshToken refreshToken) => await dbContext.RefreshTokens.AddAsync(refreshToken);
}