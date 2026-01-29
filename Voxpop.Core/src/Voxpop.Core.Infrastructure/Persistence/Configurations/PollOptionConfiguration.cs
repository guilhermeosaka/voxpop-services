using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.Polls;

namespace Voxpop.Core.Infrastructure.Persistence.Configurations;

public class PollOptionConfiguration : IEntityTypeConfiguration<PollOption>
{
    public void Configure(EntityTypeBuilder<PollOption> builder)
    {
        builder.ToTable("poll_options");

        builder.HasKey(up => up.Id);
        
        builder.HasIndex(up => new { up.PollId, up.Value }).IsUnique();

        builder.Property(up => up.Id).HasColumnName("id").IsRequired();
        builder.Property(up => up.PollId).HasColumnName("poll_id").IsRequired();
        builder.Property(up => up.Value).HasColumnName("value").HasMaxLength(128).IsRequired();
    }
}