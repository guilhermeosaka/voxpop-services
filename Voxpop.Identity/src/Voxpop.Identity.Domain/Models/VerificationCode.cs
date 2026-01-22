using System.Security.Cryptography;
using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Domain.Models;

public class VerificationCode(string target, VerificationCodeChannel channel, Guid? userId = null)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid? UserId { get; private set; } = userId;
    public string Target { get; private set; } = target;
    public VerificationCodeChannel Channel { get; private set; } = channel;
    public string Code { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? UsedAt { get; private set; }
    
    public void RefreshCode(TimeSpan expiresIn)
    {
        var value = RandomNumberGenerator.GetInt32(0, 1_000_000);
        Code = value.ToString("D6");
        ExpiresAt = DateTime.UtcNow.Add(expiresIn);
    }
    
    public void MarkAsUsed(DateTime? usedAt = null) => UsedAt = usedAt ?? DateTime.UtcNow;
}