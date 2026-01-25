using Voxpop.Profile.Domain.Abstractions;

namespace Voxpop.Profile.Domain.Models;

public class UserProfile(
    Guid id, 
    Guid userId,
    DateTime? dateOfBirth,
    Guid? genderId,
    Guid? locationId,
    Guid? educationLevelId,
    Guid? religionId,
    Guid? occupationId,
    Guid? raceId,
    Guid? ethnicityId) : BaseModel(id)
{
    public Guid UserId { get; private set; } = userId;
    public DateTime? DateOfBirth { get; private set; } = dateOfBirth;
    public Guid? GenderId { get; private set; } = genderId;
    public Guid? LocationId { get; private set; } = locationId;
    public Guid? EducationLevelId { get; private set; } = educationLevelId;
    public Guid? ReligionId { get; private set; } = religionId;
    public Guid? OccupationId { get; private set; } = occupationId;
    public Guid? RaceId { get; private set; } = raceId;
    public Guid? EthnicityId { get; private set; } = ethnicityId;
}