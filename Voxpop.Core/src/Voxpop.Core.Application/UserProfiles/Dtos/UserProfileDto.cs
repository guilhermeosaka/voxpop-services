namespace Voxpop.Core.Application.UserProfiles.Dtos;

public record UserProfileDto(
    PersonalInfoDto? PersonalInfo,
    LocationInfoDto? LocationInfo,
    ProfessionalInfoDto? ProfessionalInfo,
    CulturalInfoDto? CulturalInfo
);