using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Infrastructure.Persistence.Configurations;

public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.ToTable("VerificationCodes");
        
        builder.HasKey(vc => vc.Id);

        builder.HasIndex(vc => new { vc.Target, vc.Channel }).IsUnique();
    }
}