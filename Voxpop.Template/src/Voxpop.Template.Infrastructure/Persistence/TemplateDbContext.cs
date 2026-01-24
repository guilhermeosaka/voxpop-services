using Microsoft.EntityFrameworkCore;

namespace Voxpop.Template.Infrastructure.Persistence;

public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}