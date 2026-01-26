using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Profile.Domain.Models;

namespace Voxpop.Profile.Infrastructure.Persistence.Configurations;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("States");

        builder.HasKey(s => s.Id);
        
        builder.HasIndex(t => new { t.Code, t.CountryId }).IsUnique();
        
        builder.Property(t => t.Code).IsRequired();
        builder.Property(s => s.CountryId).IsRequired();
    }
}