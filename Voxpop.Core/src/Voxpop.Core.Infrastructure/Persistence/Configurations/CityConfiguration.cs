using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.ReferenceData;

namespace Voxpop.Core.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("cities");

        builder.HasKey(c => c.Id);

        builder.HasIndex(c => new { c.Code, c.StateId }).IsUnique();

        builder.Property(c => c.Id).HasColumnName("id").IsRequired();
        builder.Property(c => c.Code).HasColumnName("code").IsRequired();
        builder.Property(c => c.StateId).HasColumnName("state_id").IsRequired();
    }
}