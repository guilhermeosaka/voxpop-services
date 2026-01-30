using Voxpop.Core.Domain.Common.Entities;
using Voxpop.Core.Domain.Common.Interfaces;

namespace Voxpop.Core.Domain.Profiles.Entities;

public class Profile(
    Guid id,
    Guid userId) : Entity(id), IAuditable, IArchivable, IAggregateRoot
{
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public bool IsArchived { get; set; }
    public Guid? ArchivedBy { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }
    
    public Guid UserId { get; private set; } = userId;
    public DateOnly? DateOfBirth { get; private set; }
    public Guid? GenderId { get; private set; }
    public Guid? CityId { get; private set; }
    public Guid? StateId { get; private set; }
    public Guid? CountryId { get; private set; }
    public Guid? EducationLevelId { get; private set; }
    public Guid? OccupationId { get; private set; }
    public Guid? ReligionId { get; private set; }
    public Guid? RaceId { get; private set; }
    public Guid? EthnicityId { get; private set; }

    public static Profile Create(Guid userId) => new(Guid.NewGuid(), userId);

    public void UpdatePersonalInfo(PersonalInfo personalInfo) => 
        (DateOfBirth, GenderId) = personalInfo;
    public void UpdateLocation(LocationInfo locationInfo) => 
        (CityId, StateId, CountryId) = locationInfo;

    public void UpdateProfessionalInfo(ProfessionalInfo professionalInfo) =>
        (EducationLevelId, OccupationId) = professionalInfo;

    public void UpdateCulturalInfo(CulturalInfo culturalInfo) => 
        (ReligionId, RaceId, EthnicityId) = culturalInfo;
}