using Microsoft.EntityFrameworkCore;
using Voxpop.Profile.Domain.Models;
using Voxpop.Profile.Infrastructure.Persistence.Configurations;
using Voxpop.Profile.Infrastructure.Persistence.Configurations.Abstractions;

namespace Voxpop.Profile.Infrastructure.Persistence;

public class ProfileDbContext(DbContextOptions<ProfileDbContext> options) : DbContext(options)
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Gender>("Genders"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<EducationLevel>("EducationLevels"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Religion>("Religions"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Occupation>("Occupations"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Race>("Races"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Ethnicity>("Ethnicities"));
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Continent>("Continents"));
    }
}