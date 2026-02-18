using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.Polls.Entities;
using Voxpop.Core.Domain.Votes.Entities;
using Voxpop.Packages.Extensions;

namespace Voxpop.Core.Infrastructure.Persistence.Votes.Configurations;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ToTable("votes");

        builder.HasKey(up => up.Id);
        
        builder.Property(up => up.Id).HasColumnName("id").IsRequired();
        builder.Property(up => up.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(up => up.PollId).HasColumnName("poll_id").IsRequired();
        builder.Property(up => up.SlotIndex).HasColumnName("slot_index").IsRequired();
        builder.Property(up => up.OptionId).HasColumnName("option_id").IsRequired();
        builder.Property(up => up.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(up => up.CreatedAt).HasColumnName("created_at").WithUtcConverter().IsRequired();
        builder.Property(up => up.ModifiedBy).HasColumnName("modified_by").IsRequired();
        builder.Property(up => up.ModifiedAt).HasColumnName("modified_at").WithUtcConverter().IsRequired();
        
        builder.HasOne<Poll>().WithMany().HasForeignKey(up => up.PollId);
        builder.HasOne<PollOption>().WithMany().HasForeignKey(up => up.OptionId);

        builder.HasIndex(v => new { v.PollId, v.UserId, v.SlotIndex }).IsUnique();

        builder.HasIndex(v => new { v.UserId, v.PollId });
    }
}