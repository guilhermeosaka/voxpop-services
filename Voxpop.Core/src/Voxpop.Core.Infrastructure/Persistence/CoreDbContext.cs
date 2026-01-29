using Microsoft.EntityFrameworkCore;
using Voxpop.Core.Domain.Polls;
using Voxpop.Core.Domain.ReferenceData;
using Voxpop.Core.Domain.ReferenceData.Translations;
using Voxpop.Core.Domain.UserProfiles;
using Voxpop.Core.Infrastructure.Persistence.Configurations;
using Voxpop.Core.Infrastructure.Persistence.Configurations.Abstractions;
using Voxpop.Core.Infrastructure.Persistence.Interceptors;

namespace Voxpop.Core.Infrastructure.Persistence;

public class CoreDbContext(DbContextOptions<CoreDbContext> options, AuditSaveChangesInterceptor auditInterceptor) : DbContext(options)
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Poll> Polls { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new PollConfiguration());
        modelBuilder.ApplyConfiguration(new PollOptionConfiguration());
        
        modelBuilder.ApplyConfiguration(new ReferenceEntityConfiguration<Gender>("genders"));
        modelBuilder.ApplyConfiguration(new ReferenceEntityConfiguration<EducationLevel>("education_levels"));
        modelBuilder.ApplyConfiguration(new ReferenceEntityConfiguration<Religion>("religions"));
        modelBuilder.ApplyConfiguration(new ReferenceEntityConfiguration<Occupation>("occupations"));
        modelBuilder.ApplyConfiguration(new ReferenceEntityConfiguration<Race>("races"));
        modelBuilder.ApplyConfiguration(new ReferenceEntityConfiguration<Ethnicity>("ethnicities"));
        modelBuilder.ApplyConfiguration(new ReferenceEntityConfiguration<Continent>("continents"));
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());

        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<GenderTranslation>("gender_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<EducationLevelTranslation>("education_level_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<ReligionTranslation>("religion_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<OccupationTranslation>("occupation_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<RaceTranslation>("race_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<EthnicityTranslation>("ethnicity_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<ContinentTranslation>("continent_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<CountryTranslation>("country_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<StateTranslation>("state_translations"));
        modelBuilder.ApplyConfiguration(new TranslationEntityConfiguration<CityTranslation>("city_translations"));
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditInterceptor);
        
        base.OnConfiguring(optionsBuilder);
    }
}