using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Profile.Domain.Models;

namespace Voxpop.Profile.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(c => c.Id);

        builder.HasIndex(t => new { t.Code, t.StateId }).IsUnique();
        
        builder.Property(t => t.Code).IsRequired();
        builder.Property(c => c.StateId).IsRequired();
    }
}