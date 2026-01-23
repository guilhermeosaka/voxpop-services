using Microsoft.EntityFrameworkCore;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(IdentityDbContext dbContext) : IRefreshTokenRepository
{
    public async Task<IReadOnlyList<RefreshToken>> GetAllActiveByTokenId(string tokenId) =>
        await dbContext.RefreshTokens.Where(rt => rt.TokenId == tokenId && 
                                                  rt.ExpiresAt > DateTime.UtcNow && 
                                                  !rt.IsRevoked).ToListAsync();

    public async Task AddAsync(RefreshToken refreshToken) => await dbContext.RefreshTokens.AddAsync(refreshToken);
}