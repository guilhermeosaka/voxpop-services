using Microsoft.EntityFrameworkCore;

namespace Voxpop.Profile.Infrastructure.Persistence;

public class ProfileDbContext(DbContextOptions<ProfileDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}