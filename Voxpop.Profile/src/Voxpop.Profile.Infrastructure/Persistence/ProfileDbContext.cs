using Microsoft.EntityFrameworkCore;
using Voxpop.Profile.Domain.ReferenceData;
using Voxpop.Profile.Domain.UserProfiles;
using Voxpop.Profile.Infrastructure.Persistence.Configurations;
using Voxpop.Profile.Infrastructure.Persistence.Configurations.Abstractions;
using Voxpop.Profile.Infrastructure.Persistence.Interceptors;

namespace Voxpop.Profile.Infrastructure.Persistence;

public class ProfileDbContext(DbContextOptions<ProfileDbContext> options, AuditSaveChangesInterceptor auditInterceptor) : DbContext(options)
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Gender>("genders"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<EducationLevel>("education_levels"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Religion>("religions"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Occupation>("occupations"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Race>("races"));
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Ethnicity>("ethnicities"));
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new BaseCodeModelConfiguration<Continent>("continents"));
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditInterceptor);
        
        base.OnConfiguring(optionsBuilder);
    }
}