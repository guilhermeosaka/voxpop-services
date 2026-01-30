using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.Polls.Entities;
using Voxpop.Core.Infrastructure.Extensions;

namespace Voxpop.Core.Infrastructure.Persistence.Polls.Configurations;

public class PollConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.ToTable("polls");

        builder.HasKey(up => up.Id);
        
        builder.Property(up => up.Id).HasColumnName("id").IsRequired();
        builder.Property(up => up.Question).HasColumnName("question").HasMaxLength(512).IsRequired();
        builder.Property(up => up.VoteMode).HasColumnName("vote_mode").IsRequired();
        builder.Property(up => up.ExpiresAt).HasColumnName("expires_at").WithUtcConverter().IsRequired(false);
        builder.Property(up => up.IsClosed).HasColumnName("is_closed").IsRequired();
        builder.Property(up => up.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(up => up.CreatedAt).HasColumnName("created_at").WithUtcConverter().IsRequired();
        builder.Property(up => up.ModifiedBy).HasColumnName("modified_by").IsRequired();
        builder.Property(up => up.ModifiedAt).HasColumnName("modified_at").WithUtcConverter().IsRequired();
        builder.Property(up => up.IsArchived).HasColumnName("is_archived").IsRequired();
        builder.Property(up => up.ArchivedBy).HasColumnName("archived_by").IsRequired(false);
        builder.Property(up => up.ArchivedAt).HasColumnName("archived_at").WithUtcConverter().IsRequired(false);

        builder.HasMany(p => p.Options).WithOne().HasForeignKey(o => o.PollId).OnDelete(DeleteBehavior.Cascade);
        
        builder.Navigation(c => c.Options).HasField("_options").UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}