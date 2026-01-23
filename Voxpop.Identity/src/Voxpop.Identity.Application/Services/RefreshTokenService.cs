using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Application.Services;

public class RefreshTokenService(
    IHasher hasher,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork,
    IOptions<RefreshTokenOptions> options)
{
    public async Task<string> CreateAsync(Guid userId, CancellationToken ct = default)
    {
        var (newToken, newRefreshToken) = GenerateRefreshToken(userId);
        await refreshTokenRepository.AddAsync(newRefreshToken);
        await unitOfWork.SaveChangesAsync(ct);
        
        return newToken;
    }
    
    public async Task<(Guid? userId, string? token)> RefreshAsync(string token, CancellationToken ct = default)
    {
        var tokenId = hasher.Hash(token)[..16];

        var refreshTokens = await refreshTokenRepository.GetAllActiveByTokenId(tokenId);
        var refreshToken = refreshTokens.FirstOrDefault(rt => hasher.Verify(rt.TokenHash, token));

        if (refreshToken == null)
            return (null, null);

        refreshToken.Revoke();
        
        var (newToken, newRefreshToken) = GenerateRefreshToken(refreshToken.UserId);
        await refreshTokenRepository.AddAsync(newRefreshToken);
        await unitOfWork.SaveChangesAsync(ct);

        return (refreshToken.UserId, newToken);
    }

    private (string token, RefreshToken refreshToken) GenerateRefreshToken(Guid userId)
    {
        var newToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var newTokenHash = hasher.Hash(newToken);

        var newRefreshToken = RefreshToken.Create(
            userId, 
            newTokenHash[..16], 
            newTokenHash,
            DateTime.UtcNow + options.Value.ExpiresIn);

        return (newToken, newRefreshToken);
    }
}