using Voxpop.Core.Domain.Profiles.Entities;

namespace Voxpop.Core.Api.Requests;

public record SaveProfileRequest(
    PersonalInfo? PersonalInfo,
    LocationInfo? LocationInfo,
    ProfessionalInfo? ProfessionalInfo,
    CulturalInfo? CulturalInfo
);