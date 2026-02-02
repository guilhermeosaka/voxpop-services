using Voxpop.Core.Contracts.Dtos;

namespace Voxpop.Core.Contracts.Responses;

public record GetProfileResponse(
    PersonalInfoDto? PersonalInfo,
    LocationInfoDto? LocationInfo,
    ProfessionalInfoDto? ProfessionalInfo,
    CulturalInfoDto? CulturalInfo
);