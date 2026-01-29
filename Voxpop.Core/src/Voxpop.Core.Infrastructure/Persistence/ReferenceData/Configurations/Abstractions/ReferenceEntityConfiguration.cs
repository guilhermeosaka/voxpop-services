using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.ReferenceData.Abstractions;

namespace Voxpop.Core.Infrastructure.Persistence.ReferenceData.Configurations.Abstractions;

public class ReferenceEntityConfiguration<T>(string tableName) : IEntityTypeConfiguration<T> where T : ReferenceEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(tableName);

        builder.HasKey(t => t.Id);
        
        builder.HasIndex(t => t.Code).IsUnique();

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Code).HasColumnName("code").IsRequired();
    }
}