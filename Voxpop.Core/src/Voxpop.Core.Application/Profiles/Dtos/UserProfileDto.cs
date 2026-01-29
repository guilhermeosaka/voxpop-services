namespace Voxpop.Core.Application.Profiles.Dtos;

public record UserProfileDto(
    PersonalInfoDto? PersonalInfo,
    LocationInfoDto? LocationInfo,
    ProfessionalInfoDto? ProfessionalInfo,
    CulturalInfoDto? CulturalInfo
);