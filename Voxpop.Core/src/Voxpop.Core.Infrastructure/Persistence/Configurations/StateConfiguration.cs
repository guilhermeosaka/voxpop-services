using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.ReferenceData;

namespace Voxpop.Core.Infrastructure.Persistence.Configurations;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("states");

        builder.HasKey(s => s.Id);
        
        builder.HasIndex(s => new { s.Code, s.CountryId }).IsUnique();
        
        builder.Property(s => s.Id).HasColumnName("id").IsRequired();
        builder.Property(s => s.Code).HasColumnName("code").IsRequired();
        builder.Property(s => s.CountryId).HasColumnName("country_id").IsRequired();
    }
}