using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.ReferenceData;

namespace Voxpop.Core.Infrastructure.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");

        builder.HasKey(c => c.Id);
        
        builder.HasIndex(c => c.Code).IsUnique();
        
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();
        builder.Property(c => c.Code).HasColumnName("code").IsRequired();
        builder.Property(c => c.ContinentId).HasColumnName("continent_id").IsRequired();
    }
}