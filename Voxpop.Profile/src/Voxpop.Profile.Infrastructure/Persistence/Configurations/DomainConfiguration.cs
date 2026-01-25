using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Infrastructure.Persistence.Configurations;

public class DomainConfiguration<T>(string tableName) : IEntityTypeConfiguration<T> where T : BaseModel
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(tableName);

        builder.HasKey(t => t.Id);
    }
}