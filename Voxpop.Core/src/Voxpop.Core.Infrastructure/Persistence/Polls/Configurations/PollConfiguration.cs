using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Voxpop.Core.Domain.Polls.Entities;

namespace Voxpop.Core.Infrastructure.Persistence.Polls.Configurations;

public class PollConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.ToTable("polls");

        builder.HasKey(up => up.Id);

        var utcConverter = new ValueConverter<DateTimeOffset, DateTimeOffset>(
            v => v.ToUniversalTime(),
            v => v.UtcDateTime
        );
        
        builder.Property(up => up.Id).HasColumnName("id").IsRequired();
        builder.Property(up => up.Question).HasColumnName("question").HasMaxLength(512).IsRequired();
        builder.Property(up => up.ExpiresAt).HasColumnName("expires_at").HasConversion(utcConverter).IsRequired(false);
        builder.Property(up => up.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(up => up.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(up => up.ModifiedBy).HasColumnName("modified_by").IsRequired();
        builder.Property(up => up.ModifiedAt).HasColumnName("modified_at").IsRequired();
        builder.Property(up => up.IsArchived).HasColumnName("is_archived").IsRequired();
        builder.Property(up => up.ArchivedBy).HasColumnName("archived_by").IsRequired(false);
        builder.Property(up => up.ArchivedAt).HasColumnName("archived_at").IsRequired(false);

        builder.HasMany(p => p.Options).WithOne().HasForeignKey(o => o.PollId).OnDelete(DeleteBehavior.Cascade);
    }
}