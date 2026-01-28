namespace Voxpop.Identity.Domain.Models;

public class RefreshToken(Guid id, Guid userId, string tokenId, string tokenHash, DateTime expiresAt)
{
    public Guid Id { get; private set; } = id;
    public Guid UserId { get; private set; } = userId;
    public string TokenId { get; private set; } = tokenId;
    public string TokenHash { get; private set; } = tokenHash;
    public DateTime ExpiresAt { get; private set; } = expiresAt;
    public bool IsRevoked { get; private set; }

    public bool IsExpired => ExpiresAt < DateTime.UtcNow;

    public void Revoke() => IsRevoked = true;

    public static RefreshToken Create(Guid userId, string tokenId, string tokenHash, DateTime expiresAt) =>
        new(Guid.NewGuid(), userId, tokenId, tokenHash, expiresAt);
}