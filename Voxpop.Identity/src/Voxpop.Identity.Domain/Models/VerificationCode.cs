using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Domain.Models;

public class VerificationCode(string target, VerificationCodeChannel channel)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Target { get; private set; } = target;
    public VerificationCodeChannel Channel { get; private set; } = channel;
    public string CodeHash { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? UsedAt { get; private set; }
    
    public bool IsExpired => UsedAt.HasValue || ExpiresAt <= DateTime.UtcNow;
    public bool IsConsumed => UsedAt.HasValue;
    
    public void RefreshCode(string codeHash, TimeSpan expiresIn, DateTime? utcNow = null)
    {
        CodeHash = codeHash;
        ExpiresAt = (utcNow ?? DateTime.UtcNow).Add(expiresIn);
        UsedAt = null;
    }
    
    public void Consume(DateTime? usedAt = null) => UsedAt = usedAt ?? DateTime.UtcNow;
}