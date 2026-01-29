using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Infrastructure.Persistence.Configurations.Abstractions;

public class TranslationEntityConfiguration<T>(string tableName)
    : IEntityTypeConfiguration<T> where T : TranslationEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(tableName);

        builder.HasKey(t => new { t.Id, t.Language });

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Language).HasColumnName("language").IsRequired();
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
    }
}