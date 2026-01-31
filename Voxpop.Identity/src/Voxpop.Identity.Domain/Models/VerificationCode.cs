using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Domain.Models;

public class VerificationCode(Guid id, string target, VerificationCodeChannel channel)
{
    public Guid Id { get; private set; } = id;
    public string Target { get; private set; } = target;
    public VerificationCodeChannel Channel { get; private set; } = channel;
    public string CodeHash { get; private set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? UsedAt { get; private set; }

    public bool IsExpired => ExpiresAt <= DateTimeOffset.UtcNow;
    public bool IsConsumed => UsedAt.HasValue;

    public void RefreshCode(string codeHash, TimeSpan expiresIn, DateTimeOffset? utcNow = null)
    {
        CodeHash = codeHash;
        ExpiresAt = (utcNow ?? DateTimeOffset.UtcNow).Add(expiresIn);
        UsedAt = null;
    }

    public void Consume(DateTimeOffset? usedAt = null) => UsedAt = usedAt ?? DateTimeOffset.UtcNow;

    public static VerificationCode Create(string target, VerificationCodeChannel channel) =>
        new(Guid.NewGuid(), target, channel);
}