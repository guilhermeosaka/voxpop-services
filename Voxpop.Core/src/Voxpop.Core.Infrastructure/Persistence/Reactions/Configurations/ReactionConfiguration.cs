using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.Polls.Entities;
using Voxpop.Core.Domain.Reactions.Entities;
using Voxpop.Core.Infrastructure.Extensions;

namespace Voxpop.Core.Infrastructure.Persistence.Reactions.Configurations;

public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
{
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
        builder.ToTable("reactions");

        builder.HasKey(up => up.Id);
        
        builder.Property(up => up.Id).HasColumnName("id").IsRequired();
        builder.Property(up => up.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(up => up.PollId).HasColumnName("poll_id").IsRequired();
        builder.Property(up => up.ReactionType).HasColumnName("reaction_type").IsRequired();
        builder.Property(up => up.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(up => up.CreatedAt).HasColumnName("created_at").WithUtcConverter().IsRequired();
        builder.Property(up => up.ModifiedBy).HasColumnName("modified_by").IsRequired();
        builder.Property(up => up.ModifiedAt).HasColumnName("modified_at").WithUtcConverter().IsRequired();
        
        builder.HasOne<Poll>().WithMany().HasForeignKey(up => up.PollId);

        builder.HasIndex(v => new { v.UserId, v.PollId });
    }
}