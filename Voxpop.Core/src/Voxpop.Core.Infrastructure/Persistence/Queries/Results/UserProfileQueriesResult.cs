namespace Voxpop.Core.Infrastructure.Persistence.Queries.Results;

public record UserProfileQueriesResult(
    DateTime? DateOfBirth,
    Guid? GenderId,
    string? GenderName,
    Guid? CityId,
    string? CityName,
    Guid? StateId,
    string? StateName,
    Guid? CountryId,
    string? CountryName,
    Guid? ContinentId,
    string? ContinentName,
    Guid? EducationLevelId,
    string? EducationLevelName,
    Guid? OccupationId,
    string? OccupationName,
    Guid? ReligionId,
    string? ReligionName,
    Guid? RaceId,
    string? RaceName,
    Guid? EthnicityId,
    string? EthnicityName    
);