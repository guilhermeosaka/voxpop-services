namespace Voxpop.Core.Application.Dtos;

public record UserProfileDto(
    PersonalInfoDto? PersonalInfo,
    LocationInfoDto? LocationInfo,
    ProfessionalInfoDto? ProfessionalInfo,
    CulturalInfoDto? CulturalInfo
);