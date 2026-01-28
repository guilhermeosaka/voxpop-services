using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voxpop.Profile.Domain.ReferenceData;
using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles");

        builder.HasKey(up => up.Id);

        builder.Property(up => up.Id).HasColumnName("id").IsRequired();
        builder.Property(up => up.UserId).HasColumnName("user_id").IsRequired();
        
        builder.HasOne<Gender>().WithMany().HasForeignKey(up => up.GenderId);
        builder.HasOne<City>().WithMany().HasForeignKey(up => up.CityId);
        builder.HasOne<State>().WithMany().HasForeignKey(up => up.StateId);
        builder.HasOne<Country>().WithMany().HasForeignKey(up => up.CountryId);
        builder.HasOne<EducationLevel>().WithMany().HasForeignKey(up => up.EducationLevelId);
        builder.HasOne<Occupation>().WithMany().HasForeignKey(up => up.OccupationId);
        builder.HasOne<Religion>().WithMany().HasForeignKey(up => up.ReligionId);
        builder.HasOne<Race>().WithMany().HasForeignKey(up => up.RaceId);
        builder.HasOne<Ethnicity>().WithMany().HasForeignKey(up => up.EthnicityId);
        
        builder.Property(up => up.DateOfBirth).HasColumnName("date_of_birth").IsRequired(false);
        builder.Property(up => up.GenderId).HasColumnName("gender_id").IsRequired(false);
        builder.Property(up => up.CityId).HasColumnName("city_id").IsRequired(false);
        builder.Property(up => up.StateId).HasColumnName("state_id").IsRequired(false);
        builder.Property(up => up.CountryId).HasColumnName("country_id").IsRequired(false);
        builder.Property(up => up.EducationLevelId).HasColumnName("education_level_id").IsRequired(false);
        builder.Property(up => up.OccupationId).HasColumnName("occupation_id").IsRequired(false);
        builder.Property(up => up.ReligionId).HasColumnName("religion_id").IsRequired(false);
        builder.Property(up => up.RaceId).HasColumnName("race_id").IsRequired(false);
        builder.Property(up => up.EthnicityId).HasColumnName("ethnicity_id").IsRequired(false);
        builder.Property(up => up.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(up => up.ModifiedAt).HasColumnName("modified_at").IsRequired();
        builder.Property(up => up.IsArchived).HasColumnName("is_archived").IsRequired();
        builder.Property(up => up.ArchivedAt).HasColumnName("archived_at").IsRequired(false);
    }
}