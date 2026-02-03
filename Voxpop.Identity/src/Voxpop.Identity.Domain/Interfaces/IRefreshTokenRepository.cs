using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenId(string tokenId);
    Task AddAsync(RefreshToken refreshToken);
}