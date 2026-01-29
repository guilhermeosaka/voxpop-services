using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Domain.UserProfiles;

public class UserProfile(
    Guid id,
    Guid userId) : AuditableEntity(id)
{
    public Guid UserId { get; private set; } = userId;
    public DateTime? DateOfBirth { get; private set; }
    public Guid? GenderId { get; private set; }
    public Guid? CityId { get; private set; }
    public Guid? StateId { get; private set; }
    public Guid? CountryId { get; private set; }
    public Guid? EducationLevelId { get; private set; }
    public Guid? OccupationId { get; private set; }
    public Guid? ReligionId { get; private set; }
    public Guid? RaceId { get; private set; }
    public Guid? EthnicityId { get; private set; }

    public static UserProfile Create(Guid userId) => new(Guid.NewGuid(), userId);

    public void UpdatePersonalInfo(PersonalInfo personalInfo) => 
        (DateOfBirth, GenderId) = personalInfo;
    public void UpdateLocation(LocationInfo locationInfo) => 
        (CityId, StateId, CountryId) = locationInfo;

    public void UpdateProfessionalInfo(ProfessionalInfo professionalInfo) =>
        (EducationLevelId, OccupationId) = professionalInfo;

    public void UpdateCulturalInfo(CulturalInfo culturalInfo) => 
        (ReligionId, RaceId, EthnicityId) = culturalInfo;
}