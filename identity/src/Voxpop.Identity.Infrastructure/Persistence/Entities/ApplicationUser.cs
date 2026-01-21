using Microsoft.AspNetCore.Identity;

namespace Voxpop.Identity.Infrastructure.Persistence.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public bool IsService { get; set; }
    public string? VerificationCode { get; set; }
    public DateTime? VerificationCodeExpiresAt { get; set; }
}