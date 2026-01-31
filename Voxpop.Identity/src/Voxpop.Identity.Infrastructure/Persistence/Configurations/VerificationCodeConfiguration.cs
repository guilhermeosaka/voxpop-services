using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Identity.Domain.Models;
using Voxpop.Packages.Extensions;

namespace Voxpop.Identity.Infrastructure.Persistence.Configurations;

public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.ToTable("verification_codes");
        
        builder.HasKey(vc => vc.Id);

        builder.HasIndex(vc => new { vc.Target, vc.Channel }).IsUnique();
        
        builder.Property(vc => vc.Id).HasColumnName("id").IsRequired();
        builder.Property(vc => vc.Target).HasColumnName("target").IsRequired();
        builder.Property(vc => vc.Channel).HasColumnName("channel").IsRequired();
        builder.Property(vc => vc.CodeHash).HasColumnName("code_hash").IsRequired();
        builder.Property(vc => vc.ExpiresAt).HasColumnName("expires_at").WithUtcConverter().IsRequired();
        builder.Property(vc => vc.UsedAt).HasColumnName("used_at").WithUtcConverter().IsRequired(false);
    }
}