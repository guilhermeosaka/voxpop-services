using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<IReadOnlyList<RefreshToken>> GetAllActiveByTokenId(string tokenId);
    Task AddAsync(RefreshToken refreshToken);
}