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
    
    public async Task<(Guid? UserId, string? Token)> RefreshAsync(string token, CancellationToken ct = default)
    {
        var (tokenId, secret) = Parse(token);
        
        if (tokenId == null || secret == null)
            return (null, null);
        
        var refreshToken = await refreshTokenRepository.GetByTokenId(tokenId);

        if (refreshToken == null || 
            refreshToken.IsExpired || 
            refreshToken.IsRevoked ||
            !hasher.Verify(refreshToken.TokenHash, secret))
            return (null, null);

        refreshToken.Revoke();
        
        var (newToken, newRefreshToken) = GenerateRefreshToken(refreshToken.UserId);
        await refreshTokenRepository.AddAsync(newRefreshToken);
        await unitOfWork.SaveChangesAsync(ct);

        return (refreshToken.UserId, newToken);
    }

    private (string Token, RefreshToken RefreshToken) GenerateRefreshToken(Guid userId)
    {
        var newToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var newTokenHash = hasher.Hash(newToken);
        var tokenId = Ulid.NewUlid().ToString();

        var newRefreshToken = RefreshToken.Create(
            userId, 
            tokenId, 
            newTokenHash,
            DateTime.UtcNow + options.Value.ExpiresIn);

        return ($"{tokenId}.{newToken}", newRefreshToken);
    }
    
    private static (string? TokenId, string? Secret) Parse(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return (null, null);

        var dotIndex = refreshToken.IndexOf('.');
        if (dotIndex <= 0 || dotIndex == refreshToken.Length - 1)
            return (null, null);

        var span = refreshToken.AsSpan();

        var tokenIdSpan = span[..dotIndex];
        var secretSpan  = span[(dotIndex + 1)..];

        if (tokenIdSpan.Length > 64 || secretSpan.Length < 32)
            return (null, null);

        return (tokenIdSpan.ToString(), secretSpan.ToString());
    }
}