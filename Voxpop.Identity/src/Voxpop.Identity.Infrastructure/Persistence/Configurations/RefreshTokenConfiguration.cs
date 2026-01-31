using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Identity.Domain.Models;
using Voxpop.Packages.Extensions;

namespace Voxpop.Identity.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        
        builder.HasKey(rt => rt.Id);
        
        builder.HasIndex(rt => new { rt.TokenId, rt.ExpiresAt });
        
        builder.Property(rt => rt.Id).HasColumnName("id");
        builder.Property(rt => rt.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(rt => rt.TokenId).HasColumnName("token_id").IsRequired();
        builder.Property(rt => rt.TokenHash).HasColumnName("token_hash").IsRequired();
        builder.Property(rt => rt.ExpiresAt).HasColumnName("expires_at").WithUtcConverter().IsRequired();
        builder.Property(rt => rt.IsRevoked).HasColumnName("is_revoked").IsRequired();
    }
}