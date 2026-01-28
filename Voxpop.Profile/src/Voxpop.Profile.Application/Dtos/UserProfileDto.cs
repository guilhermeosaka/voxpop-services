using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Application.Dtos;

public record UserProfileDto(
    PersonalInfoDto? PersonalInfo,
    LocationInfoDto? LocationInfo,
    ProfessionalInfoDto? ProfessionalInfo,
    CulturalInfoDto? CulturalInfo
);