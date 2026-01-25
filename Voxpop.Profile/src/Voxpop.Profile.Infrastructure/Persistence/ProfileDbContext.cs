using Microsoft.EntityFrameworkCore;
using Voxpop.Profile.Domain.Models;
using Voxpop.Profile.Infrastructure.Persistence.Configurations;

namespace Voxpop.Profile.Infrastructure.Persistence;

public class ProfileDbContext(DbContextOptions<ProfileDbContext> options) : DbContext(options)
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new DomainConfiguration<Gender>("Genders"));
        modelBuilder.ApplyConfiguration(new DomainConfiguration<EducationLevel>("EducationLevels"));
        modelBuilder.ApplyConfiguration(new DomainConfiguration<Religion>("Religions"));
        modelBuilder.ApplyConfiguration(new DomainConfiguration<Occupation>("Occupations"));
        modelBuilder.ApplyConfiguration(new DomainConfiguration<Race>("Races"));
        modelBuilder.ApplyConfiguration(new DomainConfiguration<Ethnicity>("Ethnicities"));
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
    }
}